using System;
namespace UIFrameWork
{
	public interface IDynamicProperty
	{
		void DoChangeProperty(int id,object newProptey,object newValue);
		PropertyItem GetProterty(int id);
	}
}

