﻿using System;

namespace Common.Helper
{
	public static class RandomHelper
	{
		public static UInt64 RandUInt64()
		{
			var bytes = new byte[8];
			Random random = new Random();
			random.NextBytes(bytes);
			return BitConverter.ToUInt64(bytes, 0);
		}
	}
}