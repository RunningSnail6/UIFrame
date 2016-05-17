using System;
using System.Collections.Generic;
using UnityEngine;


namespace UIFrameWork
{
	public class BaseActor:IDynamicProperty
	{
		protected Dictionary<int,PropertyItem> dicProperty = null;

		public event PropertyChangedHandle PropertyChanged;

		public EnumActorType ActorType{ set; get;}

		public virtual void AddProperty(EnumPropertyType propertyType,object content)
		{
			AddProperty ((int)propertyType, content);
		}

		public virtual void AddProperty(int IDictionary,object content)
		{
			PropertyItem property = new PropertyItem (IDictionary, content);
			AddProperty (property);
		}

		public virtual void AddProperty(PropertyItem property)
		{
			if(null == dicProperty)
			{
				dicProperty = new Dictionary<int, PropertyItem>();
			}
			if(dicProperty.ContainsKey(property.ID))
			{
				//remove same property
			}
			dicProperty.Add (property.ID, property);
			property.Owner = this;
		}

		public void RemoveProperty(EnumPropertyType propertyType)
		{
			RemoveProperty ((int)propertyType);
		}

		public void RemoveProperty(int id)
		{
			if (null != dicProperty && dicProperty.ContainsKey (id))
				dicProperty.Remove (id);
		}

		public void ClearProperty()
		{
			if(null != dicProperty)
			{
				dicProperty.Clear();
				dicProperty = null;
			}
		}

		public virtual PropertyItem GetProperty(EnumPropertyType propertyType)
		{
			return GetProterty ((int) propertyType);
		}

		protected virtual void OnPropertyChanged(int id, object oldValue, object newValue)
		{
			//add update ......
			if(id == (int) EnumPropertyType.HP)
			{

			}
		}

		#region IDynamicProperty implementation

		public void DoChangeProperty (int id, object oldValue, object newValue)
		{
			OnPropertyChanged (id, oldValue, newValue);
			if (null != PropertyChanged)
				PropertyChanged (this, id, oldValue, newValue);
		}

		public PropertyItem GetProterty (int id)
		{
			if (null == dicProperty)
				return null;
			if (dicProperty.ContainsKey (id))
				return dicProperty [id];
			Debug.LogWarning("Actor dicProperty non Property ID:" + id);
			return null;
		}

		#endregion

		public BaseActor ()
		{
		}

	}
}

