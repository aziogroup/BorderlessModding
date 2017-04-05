#include <stdio.h>
#include <Windows.h>

int main()
{
	printf("èoóÕÇäJén");
	getchar();
	
	HWND hWnd = FindWindow(NULL, L"BorderLessEr");
	
	long bef_style = GetWindowLong(hWnd, GWL_STYLE);

	SetWindowLong(hWnd, GWL_STYLE, WS_POPUP);

	//SetWindowPos(hWnd, NULL, 0, 0, 0, 0, (SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED));
	ShowWindow(hWnd, SW_SHOW);

	getchar();

	SetWindowLong(hWnd, GWL_STYLE, bef_style);

	//SetWindowPos(hWnd, NULL, 0, 0, 0, 0, (SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED));
	ShowWindow(hWnd, SW_SHOW);
}