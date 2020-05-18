using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BorderLessEr
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();

            this.RefreshProcessList();
        }

        /// <summary>
        /// ウィンドウ表示スタイル
        /// </summary>
        enum ShowWindowStyle : int
        {
            SW_SHOW = 1,
            SW_SHOWMINNOACTIVE = 7,
            SW_RESTORE = 9,
            GWL_STYLE = -16,
            SWP_FRAMECHANGED = 0x0020
        };

        /// <summary>
        /// ウィンドウスタイル
        /// </summary>
        enum WindowStyle : uint
        {
            WS_POPUP = 0x80000000,
            WS_OVERLAPPEDWINDOW = 0x00CF0000,
        };

        /// <summary>
        /// プロセス一覧
        /// </summary>
        readonly List<System.Diagnostics.Process> processList = new List<System.Diagnostics.Process>();

        /// <summary>
        /// スタイル一覧
        /// </summary>
        readonly Dictionary<string, long> styleList = new Dictionary<string, long>();

        /// <summary>
        /// サイズ一覧
        /// </summary>
        readonly Dictionary<string, RECT> sizeList = new Dictionary<string, RECT>();

        /// <summary>
        /// ウィンドウ表示
        /// </summary>
        [DllImport("user32.dll")]
        static extern private int ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// ウィンドウの属性変更
        /// </summary>
        [DllImport("user32.dll")]
        static extern private long SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);

        /// <summary>
        /// ウィンドウの属性取得
        /// </summary>
        [DllImport("user32.dll")]
        static extern private long GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// ウィンドウの位置設定
        /// </summary>
        [DllImport("user32.dll")]
        static extern private int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int width, int height, int uFlags);

        /// <summary>
        /// ウィンドウのサイズ取得
        /// </summary>
        [DllImport("user32.dll")]
        static extern private int GetWindowRect(IntPtr hwnd, out RECT rc);

        /// <summary>
        /// ウィンドウ矩形情報
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public int Width => Math.Abs(this.Right - this.Left);
            public int Height => Math.Abs(this.Bottom - this.Top);
        }

        /// <summary>
        /// フルスクリーン化解除ボタン
        /// </summary>
        private void releaseButton_Click(object sender, EventArgs e)
        {
            this.ReleaseBorderless();
        }

        /// <summary>
        /// サイズ変更時
        /// </summary>
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            // 最小化時かつタスクトレイ格納モードであった場合タスクトレイに格納
            if (this.WindowState == FormWindowState.Minimized && this.minimumCheckBox.Checked)
            {
                this.notifyIcon.Visible = true;
                this.Hide();
            }
        }

        /// <summary>
        /// タスクトレイダブルクリック時表示処理
        /// </summary>
        private void notifyIcon_DoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.notifyIcon.Visible = false;
        }

        /// <summary>
        /// 再検索
        /// </summary>
        private void refreshButton_Click(object sender, EventArgs e)
        {
            this.RefreshProcessList();
        }

        /// <summary>
        /// プロセス一覧を取得します
        /// </summary>
        private void RefreshProcessList()
        {
            //ローカルコンピュータ上で実行されているすべてのプロセスを取得
            var processList = System.Diagnostics.Process.GetProcesses();

            this.processComboBox.Items.Clear();
            this.processList.Clear();

            //配列から1つずつ取り出す
            foreach (var process in processList.OrderByDescending(s => s.Id))
            {
                try
                {
                    if (process.MainWindowHandle == IntPtr.Zero) continue;

                    //プロセス名を取得・表示
                    this.processComboBox.Items.Add($"[{process.Id:D5}] | {process.ProcessName} : {process.MainWindowTitle}");
                    this.processList.Add(process);
                }
                catch (Exception)
                {
                    this.processComboBox.Items.Add("プロセス情報取得失敗");
                }
            }

            this.processComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// ボーダーレス化開始
        /// </summary>
        private void applyButton_Click(object sender, EventArgs e)
        {
            this.ApplyBorderless();
        }

        /// <summary>
        /// 指定されたプロセスをボーダーレス化します
        /// </summary>
        private void ApplyBorderless()
        {
            if (this.styleList.ContainsKey(this.processComboBox.Text))
            {
                MessageBox.Show("すでに仮想フルスクリーン化されています");
                return;
            }

            if (this.processList[this.processComboBox.SelectedIndex].MainWindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("HWNDがNULLです");
                return;
            }

            var process = this.processList[this.processComboBox.SelectedIndex];

            // 現在のスタイル情報を退避
            this.styleList[this.processComboBox.Text] = GetWindowLong(process.MainWindowHandle, (int)ShowWindowStyle.GWL_STYLE);

            // スタイルの適用
            SetWindowLong(process.MainWindowHandle, (int)ShowWindowStyle.GWL_STYLE, (uint)WindowStyle.WS_POPUP);

            // 現在のウィンドウサイズ退避
            GetWindowRect(process.MainWindowHandle, out RECT rc);
            this.sizeList[this.processComboBox.Text] = rc;

            var screenBounds = Screen.GetBounds(new Point(rc.Left, rc.Top));

            // 座標0,0オプションが有効の場合0,0に移動させる
            if (this.locationZeroCheckBox.CheckState == CheckState.Checked)
            {
                // フルスクリーンオプション時画面全体にウィンドウを引き延ばす
                if (this.fullscreenCheckBox.CheckState == CheckState.Checked)
                {
                    SetWindowPos(process.MainWindowHandle, 0, screenBounds.Left, screenBounds.Top, screenBounds.Width, screenBounds.Height, (int)ShowWindowStyle.SWP_FRAMECHANGED);
                }
                else
                    SetWindowPos(process.MainWindowHandle, 0, screenBounds.Left, screenBounds.Top, rc.Right - rc.Left, rc.Bottom - rc.Top, (int)ShowWindowStyle.SWP_FRAMECHANGED);
            }
            else
            {
                // フルスクリーンオプション時画面全体にウィンドウを引き延ばす
                if (this.fullscreenCheckBox.CheckState == CheckState.Checked)
                {
                    SetWindowPos(process.MainWindowHandle, 0, rc.Left, rc.Top, screenBounds.Width, screenBounds.Height, (int)ShowWindowStyle.SWP_FRAMECHANGED);

                }
                else
                    SetWindowPos(process.MainWindowHandle, 0, rc.Left, rc.Top, rc.Width, rc.Height, (int)ShowWindowStyle.SWP_FRAMECHANGED);
            }

            // ウィンドウアクティブ化
            ShowWindow(process.MainWindowHandle, (int)ShowWindowStyle.SW_SHOW);
        }

        /// <summary>
        /// 指定されたプロセスのボーダーレスを解除します
        /// </summary>
        private void ReleaseBorderless()
        {
            // 元のウィンドウ情報を復元
            if (this.styleList.ContainsKey(this.processComboBox.Text))
            {
                // 属性の復元
                var restoreProcess = this.processList[this.processComboBox.SelectedIndex];
                var restoreStyle = this.styleList[this.processComboBox.Text];

                SetWindowLong(restoreProcess.MainWindowHandle, (int)ShowWindowStyle.GWL_STYLE, restoreStyle);
                this.styleList.Remove(this.processComboBox.Text);

                // サイズの復元
                var restoreSize = this.sizeList[this.processComboBox.Text];

                SetWindowPos(
                    restoreProcess.MainWindowHandle,
                    0,
                    restoreSize.Left,
                    restoreSize.Top,
                    restoreSize.Width,
                    restoreSize.Height,
                    (int)ShowWindowStyle.SWP_FRAMECHANGED);
                this.sizeList.Remove(this.processComboBox.Text);

            }
        }
    }
}
