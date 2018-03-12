using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public static class achievements{
	[DllImport("__Internal")]
	public static extern void _ReportAchievement( string achievementID, float progress );
	
}