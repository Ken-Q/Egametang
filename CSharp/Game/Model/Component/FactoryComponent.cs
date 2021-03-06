﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Base;

namespace Model
{
	public class FactoryComponent<T>: Component<World>, IAssemblyLoader where T : Entity<T>
	{
		private Dictionary<int, IFactory<T>> allConfig;

		public void Load(Assembly assembly)
		{
			this.allConfig = new Dictionary<int, IFactory<T>>();
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				object[] attrs = type.GetCustomAttributes(typeof (FactoryAttribute), false);
				if (attrs.Length == 0)
				{
					continue;
				}

				FactoryAttribute attribute = (FactoryAttribute) attrs[0];
				if (attribute.ClassType != typeof (T))
				{
					continue;
				}

				object obj = (Activator.CreateInstance(type));

				var iFactory = obj as IFactory<T>;
				if (iFactory == null)
				{
					throw new Exception($"class: {type.Name} not inherit from IFactory");
				}

				this.allConfig[attribute.Type] = iFactory;
			}
		}

		public T Create(int type, int configId)
		{
			return this.allConfig[type].Create(configId);
		}
	}
}