using System;
namespace UIFrameWork
{
	public class PropertyItem
	{
		public int ID{ get; set;}

		private object content;//属性对应的值
		private Type proertyType;

		public object Content 
		{
			get;
			set;
		}
		public PropertyItem (int id,object content)
		{
			proertyType = content.GetType ();
			ID = id;
			Content = content;
		}
	}
}

