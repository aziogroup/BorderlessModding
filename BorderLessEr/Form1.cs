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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            GetAllProcess();
        }

        readonly int
            SW_SHOW = 1,
            SW_SHOWMINNOACTIVE = 7,
            SW_RESTORE = 9,
            GWL_STYLE = -16,
            WS_OVERLAPPEDWINDOW = 0x00CF0000,
            SWP_FRAMECHANGED = 0x0020;

        readonly uint
            WS_POPUP = 0x80000000;

        //Dictionary<int, string> processList = new Dictionary<int, string>();
        List<System.Diagnostics.Process> processList = new List<System.Diagnostics.Process>();
        Dictionary<string, long> styleList = new Dictionary<string, long>();
        Dictionary<string, RECT> sizeList = new Dictionary<string, RECT>();


        [DllImport("user32.dll")]
        static extern private int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern private long SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);

        [DllImport("user32.dll")]
        static extern private long GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern private int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int width, int height, int uFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        static extern private int GetWindowRect(IntPtr hwnd, out RECT rc);

        private void button3_Click(object sender, EventArgs e)
        {
            if (styleList.ContainsKey(comboBox1.Text))
            {
                SetWindowLong(processList[comboBox1.SelectedIndex].MainWindowHandle, GWL_STYLE, styleList[comboBox1.Text]);
                styleList.Remove(comboBox1.Text);
                
                SetWindowPos(processList[comboBox1.SelectedIndex].MainWindowHandle, IntPtr.Zero,
                    sizeList[comboBox1.Text].left,
                    sizeList[comboBox1.Text].top,
                    sizeList[comboBox1.Text].right - sizeList[comboBox1.Text].left,
                    sizeList[comboBox1.Text].bottom - sizeList[comboBox1.Text].top,
                    SWP_FRAMECHANGED);
                sizeList.Remove(comboBox1.Text);

            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && checkBox3.Checked)
            {
                notifyIcon1.Visible = true;
                Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        /// <summary>
        /// フォーム読み込み時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 再検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            GetAllProcess();
        }

        /// <summary>
        /// プロセス一覧を取得します
        /// </summary>
        private void GetAllProcess()
        {
            //ローカルコンピュータ上で実行されているすべてのプロセスを取得
            System.Diagnostics.Process[] ps =
                System.Diagnostics.Process.GetProcesses();

            comboBox1.Items.Clear();
            processList.Clear();

            //配列から1つずつ取り出す
            foreach (System.Diagnostics.Process p in ps.OrderBy(s => s.Id))
            {
                try
                {
                    if (p.MainWindowHandle == IntPtr.Zero) continue;
                    //プロセス名を出力する
                    comboBox1.Items.Add($"[ID] {p.Id:D5}  |  [Name] {p.ProcessName}");

                    processList.Add(p);
                }
                catch (Exception ex)
                {
                    comboBox1.Items.Add("エラー");
                }
            }

            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// ボーダーレス化開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DoBorderless();
        }

        /// <summary>
        /// 指定されたプロセス名をボーダーレス化します
        /// </summary>
        private void DoBorderless()
        {
            if (styleList.ContainsKey(comboBox1.Text))
            {
                MessageBox.Show("すでに仮想フルスクリーン化されています");
                return;
            }
            if (processList[comboBox1.SelectedIndex].MainWindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("HWNDがNULLです");
                return;
            }
            //ShowWindow(processList[comboBox1.SelectedIndex].MainWindowHandle, SW_RESTORE);
            styleList[comboBox1.Text] = GetWindowLong(processList[comboBox1.SelectedIndex].MainWindowHandle, GWL_STYLE);

            SetWindowLong(processList[comboBox1.SelectedIndex].MainWindowHandle, GWL_STYLE, WS_POPUP);

            RECT rc;

            GetWindowRect(processList[comboBox1.SelectedIndex].MainWindowHandle, out rc);

            sizeList[comboBox1.Text] = rc;

            Rectangle scr_bounds = Screen.GetBounds(new Point(rc.left, rc.top));


            if (checkBox1.CheckState == CheckState.Checked)
            {
                if (checkBox2.CheckState == CheckState.Checked)
                {
                    SetWindowPos(processList[comboBox1.SelectedIndex].MainWindowHandle, IntPtr.Zero, scr_bounds.Left, scr_bounds.Top, scr_bounds.Width, scr_bounds.Height, SWP_FRAMECHANGED);
                }
                else
                    SetWindowPos(processList[comboBox1.SelectedIndex].MainWindowHandle, IntPtr.Zero, scr_bounds.Left, scr_bounds.Top, rc.right - rc.left, rc.bottom - rc.top, SWP_FRAMECHANGED);
            }
            else
            {
                if (checkBox2.CheckState == CheckState.Checked)
                {
                    SetWindowPos(processList[comboBox1.SelectedIndex].MainWindowHandle, IntPtr.Zero, rc.left, rc.top, scr_bounds.Width, scr_bounds.Height, SWP_FRAMECHANGED);

                }
                else
                    SetWindowPos(processList[comboBox1.SelectedIndex].MainWindowHandle, IntPtr.Zero, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, SWP_FRAMECHANGED);
            }

            ShowWindow(processList[comboBox1.SelectedIndex].MainWindowHandle, SW_SHOW);
        }


    }
}
