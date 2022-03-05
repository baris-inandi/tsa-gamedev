using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZgUtils.GenerateGameCode
{
	public class GenerateGameCode
	{
		public static string Generate(string val)
		{
			string gamecode = string.Empty;
			string[] substrings = val.Split('.');
			foreach (string str in substrings)
			{
				gamecode += $"{int.Parse(str).ToString("x")}-".ToUpper();
			}
            Debug.Log(val);
			return gamecode.Remove(gamecode.Length - 1);
		}

		public static string DecodeToIP(string gamecode)
		{
			string decodedIP = string.Empty;
			string[] substrings = gamecode.Split('-');
			foreach (string str in substrings)
			{
				decodedIP += $"{int.Parse(str, System.Globalization.NumberStyles.HexNumber)}.";
			}
			Debug.Log(decodedIP.Remove(decodedIP.Length - 1));
			return decodedIP.Remove(decodedIP.Length - 1);
		}

	}
}
