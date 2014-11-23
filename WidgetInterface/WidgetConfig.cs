using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallSwitch.WidgetInterface
{
	/// <summary>
	/// A storage place for widgets to store configuration values.
	/// This data will be persisted to the settings file.
	/// </summary>
	public sealed class WidgetConfig : ICollection<WidgetConfigItem>
	{
		private List<WidgetConfigItem> _items = new List<WidgetConfigItem>();

		/// <summary>
		/// Creates a new configuration storage object.
		/// </summary>
		public WidgetConfig()
		{
		}

		/// <summary>
		/// Gets or sets the configuration value.
		/// </summary>
		/// <param name="name">The name of the configuration item.</param>
		/// <returns>The configuration value.</returns>
		public string this[string name]
		{
			get
			{
				var s = TryGetItem(name);
				if (s == null) throw new ArgumentOutOfRangeException("name");
				return s.Value;
			}
			set
			{
				SetValue(name, value);
			}
		}

		/// <summary>
		/// Tests if the configuration item exists.
		/// </summary>
		/// <param name="name">Name of the configuration item.</param>
		/// <returns>True if the value exists; otherwise, false.</returns>
		public bool HasValue(string name)
		{
			return TryGetItem(name) != null;
		}

		/// <summary>
		/// Attempts to retrieve a configuration item.
		/// </summary>
		/// <param name="name">Name of the configuration item.</param>
		/// <returns>If it exists, the configuration item object; otherwise null.</returns>
		public WidgetConfigItem TryGetItem(string name)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

			foreach (var setting in _items)
			{
				if (setting.Name == name) return setting;
			}

			return null;
		}

		/// <summary>
		/// Gets the value of a configuration item.
		/// </summary>
		/// <param name="name">Name of the configuration item.</param>
		/// <param name="defaultValue">Default value to be returned when no item exists.</param>
		/// <returns>If the configuration item exists, its value; otherwise defaultValue.</returns>
		public string GetValue(string name, string defaultValue)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

			foreach (var setting in _items)
			{
				if (setting.Name == name) return setting.Value;
			}

			return defaultValue;
		}

		/// <summary>
		/// Gets the value of a configuration item.
		/// </summary>
		/// <param name="name">Name of the configuration item.</param>
		/// <returns>The value of the configuration item.</returns>
		public string GetValue(string name)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

			foreach (var setting in _items)
			{
				if (setting.Name == name) return setting.Value;
			}

			throw new ArgumentOutOfRangeException("name");
		}

		/// <summary>
		/// Sets the value of a configuration item.
		/// </summary>
		/// <param name="name">Name of the configuration item.</param>
		/// <param name="value">New value for the configuration item.</param>
		public void SetValue(string name, string value)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

			var setting = TryGetItem(name);
			if (setting != null)
			{
				setting.Value = value;
			}
			else
			{
				setting = new WidgetConfigItem(name, value);
				_items.Add(setting);
			}
		}

		/// <summary>
		/// Gets the number of configuration items in this collection.
		/// </summary>
		public int Count
		{
			get { return _items.Count; }
		}

		/// <summary>
		/// Returns false. This object is not read-only.
		/// </summary>
		public bool IsReadOnly
		{
			get { return false; }
		}

		/// <summary>
		/// Adds a new configuration item. If another item exists will the same name, it will be replaced.
		/// </summary>
		/// <param name="item">The configuration item to be added.</param>
		public void Add(WidgetConfigItem item)
		{
			if (item == null) throw new ArgumentNullException("item");
			Remove(item.Name);
			_items.Add(item);
		}

		/// <summary>
		/// Removes all configuration items from this collection.
		/// </summary>
		public void Clear()
		{
			_items.Clear();
		}

		/// <summary>
		/// Tests if this collection contains a configuration item.
		/// </summary>
		/// <param name="item">The configuration item to search for.</param>
		/// <returns>True if the item exists in the collection; otherwise false.</returns>
		public bool Contains(WidgetConfigItem item)
		{
			return _items.Contains(item);
		}

		/// <summary>
		/// Tests if a configuration value with the specified name exists.
		/// </summary>
		/// <param name="name">Name of the configuration item.</param>
		/// <returns>True if it exists; otherwise false.</returns>
		public bool Contains(string name)
		{
			foreach (var s in _items)
			{
				if (s.Name == name) return true;
			}
			return false;
		}

		/// <summary>
		/// Copies the contents of this collection to an array.
		/// </summary>
		/// <param name="array">Destination array.</param>
		/// <param name="arrayIndex">Copy index.</param>
		public void CopyTo(WidgetConfigItem[] array, int arrayIndex)
		{
			_items.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Removes a configuration item.
		/// </summary>
		/// <param name="item">The item to be removed.</param>
		/// <returns>True if the item exists and was removed; otherwise false.</returns>
		public bool Remove(WidgetConfigItem item)
		{
			return _items.Remove(item);
		}

		/// <summary>
		/// Removes a configuration value.
		/// </summary>
		/// <param name="name">Name of the configuration item.</param>
		/// <returns>True if it exists and was removed; otherwise false.</returns>
		public bool Remove(string name)
		{
			foreach (var s in _items)
			{
				if (s.Name == name)
				{
					_items.Remove(s);
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Gets a collection of all configuration names.
		/// </summary>
		public IEnumerable<string> Names
		{
			get
			{
				foreach (var s in _items) yield return s.Name;
			}
		}

		/// <summary>
		/// Gets a collection of all configuration values.
		/// </summary>
		public IEnumerable<string> Values
		{
			get
			{
				foreach (var s in _items) yield return s.Value;
			}
		}

		/// <summary>
		/// Gets an enumerator fort his collection.
		/// </summary>
		/// <returns>A new enumerator object.</returns>
		public IEnumerator<WidgetConfigItem> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _items.GetEnumerator();
		}
	}
}
