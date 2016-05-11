using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace UIFrameWork
{
	public class TouchHandle
	{
		private event OnTouchEventHandle eventHandle = null;
		private object[] handleParams;

		private void DestoryHandle()
		{
			if(null != eventHandle)
			{
				eventHandle -= eventHandle;
				eventHandle = null;
			}
		}

		public void SetHandle(OnTouchEventHandle _handle,params object[] _params)
		{
			DestoryHandle ();
			eventHandle += _handle;
			handleParams = _params;
		}

		public TouchHandle(OnTouchEventHandle _handle,params object[] _params)
		{
			SetHandle (_handle, _params);
		}

		public TouchHandle()
		{

		}
	}

	public class EventTriggerListener : MonoBehaviour,
	IPointerClickHandler,
	IPointerDownHandler,   
	IPointerUpHandler,  
	IPointerEnterHandler,   
	IPointerExitHandler,
	ISelectHandler,   
	IUpdateSelectedHandler,   
	IDeselectHandler,   
	IDragHandler,   
	IEndDragHandler,   
	IDropHandler,   
	IScrollHandler,   
	IMoveHandler  
	{
		public TouchHandle onClick;   
		public TouchHandle onDown;   
		public TouchHandle onEnter;   
		public TouchHandle onExit;   
		public TouchHandle onUp;   
		public TouchHandle onSelect;   
		public TouchHandle onUpdateSelect;   
		public TouchHandle onDeSelect;   
		public TouchHandle onDrag;   
		public TouchHandle onDragEnd;   
		public TouchHandle onDrop;   
		public TouchHandle onScroll;   
		public TouchHandle onMove;

		#region IPointerClickHandler implementation

		public void OnPointerClick (PointerEventData eventData)
		{
			throw new System.NotImplementedException ();
		}

		#endregion

		void Start ()
		{
		
		}
		void Update ()
		{
		
		}
	}
}
