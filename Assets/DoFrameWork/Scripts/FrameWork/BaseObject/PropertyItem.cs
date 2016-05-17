using System;
namespace UIFrameWork
{
	public class PropertyItem
	{
		public int ID{ get; set;}

		private object content;//属性对应的值
		private object rawContent;
		private bool canRandom = false;
		private int curRandomInt;
		private float curRandomFloat;
		private Type proertyType;

		//owner
		public IDynamicProperty Owner =null;

		public object Content 
		{
			get 
			{
				return GetContent();
			}
			set
			{
				if(value != GetContent())
				{
					object oldContent = GetContent();
					SetContet(value);
					if(Owner != null)
						Owner.DoChangeProperty(ID,oldContent,value);
				}
			}
		}

		public void SetValueWithoutEvent(object content)
		{
			if(content != GetContent())
			{
				object oldContent = GetContent();
				SetContet(content);
			}
		}

		public object RawContent
		{
			get{return rawContent;}
		}

		public PropertyItem (int id,object content)
		{
			proertyType = content.GetType ();
			if(proertyType == typeof(System.Int32) || proertyType == typeof(System.Single))
			{
				canRandom = true;
			}
			ID = id;
			SetContet (content);
			rawContent = content;
		}

		private void SetContet(object content)
		{
			if(canRandom)
			{
				if(proertyType == typeof(System.Int32))
				{
					curRandomInt = UnityEngine.Random.Range(1,1000);
					this.content = (int)content + curRandomInt;
				}
				else if(proertyType == typeof(System.Single))
				{
					curRandomFloat = UnityEngine.Random.Range(1.0f,1000.0f);
					this.content = (float)content + curRandomFloat;
				}
			}
			else
			{
				this.content = content;
			}
		}

		private object GetContent()
		{
			if(canRandom)
			{
				if(proertyType == typeof(System.Int32))
				{
					return (int)this.content - curRandomInt;
				}
				else if(proertyType == typeof(System.Single))
				{
					return (float)this.content - curRandomFloat;
				}
			}
				return this.content;
		}
	}
}

