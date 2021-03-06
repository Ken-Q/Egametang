﻿using System;

namespace Base
{
	public static class TimeHelper
	{
		private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		/// <summary>
		/// 客户端时间
		/// </summary>
		/// <returns></returns>
		public static long ClientNow()
		{
			return Convert.ToInt64((DateTime.UtcNow - epoch).TotalMilliseconds);
		}

		public static long ClientNowTicks()
		{	
			return Convert.ToInt64((DateTime.UtcNow - epoch).Ticks);
		}

		/// <summary>
		/// 登陆前是客户端时间,登陆后是同步过的服务器时间
		/// </summary>
		/// <returns></returns>
		public static long Now()
		{
			TimeComponent gameTimeComponent = Game.Scene?.GetComponent<TimeComponent>();
			if (gameTimeComponent == null)
			{
				return TimeHelper.ClientNow();
			}
			long t = gameTimeComponent.Now();
			return t;
		}
    }
}