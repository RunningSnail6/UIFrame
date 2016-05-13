using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace UIFrameWork
{
	public class TouchHandle
	{
		private event OnTouchEventHandle eventHandle = null;
		private object[] handleParams;

		public TouchHandle(OnTouchEventHandle _handle,params object[] _params)
		{
			SetHandle (_handle, _params);
		}
		
		public TouchHandle()
		{
			
		}


		public void SetHandle(OnTouchEventHandle _handle,params object[] _params)
		{
			DestoryHandle ();
			eventHandle += _handle;
			handleParams = _params;
		}

		public void CallEventHandle(GameObject _lsitener,object _args)
		{
			if(null != eventHandle)
			{
				eventHandle(_lsitener,_args,handleParams);
			}
		}

		public void DestoryHandle()
		{
			if(null != eventHandle)
			{
				eventHandle -= eventHandle;
				eventHandle = null;
			}
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
		public TouchHandle onDoubleClick; 
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

		/// <summary>
		/// Get the specified go.
		/// </summary>
		/// <param name="go">Go.</param>
		static public EventTriggerListener Get(GameObject go)   
		{   
//			EventTriggerListener listener = go.GetComponent<EventTriggerListener>();   
//			if (listener == null) listener = go.AddComponent<EventTriggerListener>();   
//			return listener;   
//			EventTriggerListener listener = go.GetOrAddComponent<EventTriggerListener> ();
//			return listener;

			return go.GetOrAddComponent<EventTriggerListener> ();
		} 

		void OnDestory()
		{
			this.RemoveAllHandle ();
		}

		private void RemoveAllHandle()
		{
			this.RemoveHandle (onClick);
			this.RemoveHandle (onDoubleClick);
			this.RemoveHandle (onDown);
			this.RemoveHandle (onEnter);
			this.RemoveHandle (onExit);
			this.RemoveHandle (onUp);
			this.RemoveHandle (onSelect);
			this.RemoveHandle (onUpdateSelect);
			this.RemoveHandle (onDeSelect);
			this.RemoveHandle (onDrag);
			this.RemoveHandle (onDragEnd);
			this.RemoveHandle (onDrop);
			this.RemoveHandle (onScroll);
			this.RemoveHandle (onMove);
		}

		private void RemoveHandle(TouchHandle _handle)
		{
			if(null != _handle)
			{
				_handle.DestoryHandle();
				_handle = null;
			}
		}

		#region IPointerClickHandler implementation

		public void OnPointerClick (PointerEventData eventData)
		{
			if (null != onClick)
				onClick.CallEventHandle (this.gameObject, eventData);
		}

		#endregion

		#region IPointerDownHandler implementation
		
		public void OnPointerDown (PointerEventData eventData)
		{
			if (null != onDown)
				onDown.CallEventHandle (this.gameObject, eventData);
		}
		
		#endregion

		#region IPointerUpHandler implementation

		public void OnPointerUp (PointerEventData eventData)
		{
			if (null != onEnter)
                onEnter.CallEventHandle(this.gameObject, eventData);
		}

		#endregion

		#region IPointerEnterHandler implementation

		public void OnPointerEnter (PointerEventData eventData)
		{
			if (null != onExit)
                onExit.CallEventHandle(this.gameObject, eventData);
		}

		#endregion

		#region IPointerExitHandler implementation

		public void OnPointerExit (PointerEventData eventData)
		{
			if (null != onUp)
                onUp.CallEventHandle(this.gameObject, eventData);
		}

		#endregion

		#region ISelectHandler implementation

		public void OnSelect (BaseEventData eventData)
		{
			if (null != onSelect)
                onSelect.CallEventHandle(this.gameObject, eventData);
		}

		#endregion

		#region IUpdateSelectedHandler implementation

		public void OnUpdateSelected (BaseEventData eventData)
		{
			if (null != onUpdateSelect)
                onUpdateSelect.CallEventHandle(this.gameObject, eventData);
		}

		#endregion

		#region IDeselectHandler implementation

		public void OnDeselect (BaseEventData eventData)
		{
			if (null != onDeSelect)
                onDeSelect.CallEventHandle(this.gameObject, eventData);
		}

		#endregion

		#region IDragHandler implementation

		public void OnDrag (PointerEventData eventData)
		{
			if (null != onDrag)
                onDrag.CallEventHandle(this.gameObject, eventData);
		}

		#endregion

		#region IEndDragHandler implementation

		public void OnEndDrag (PointerEventData eventData)
		{
			if (null != onDragEnd)
                onDragEnd.CallEventHandle(this.gameObject, eventData);
		}

		#endregion

		#region IDropHandler implementation

		public void OnDrop (PointerEventData eventData)
		{
			if (null != onDrop)
                onDrop.CallEventHandle(this.gameObject, eventData);
		}

		#endregion

		#region IScrollHandler implementation

		public void OnScroll (PointerEventData eventData)
		{
			if (null != onScroll)
                onScroll.CallEventHandle(this.gameObject, eventData);
		}

		#endregion

		#region IMoveHandler implementation

		public void OnMove (AxisEventData eventData)
		{
			if (null != onMove)
                onMove.CallEventHandle(this.gameObject, eventData);
		}

		#endregion

		public void SetEventHandle(EnumTouchEventType _type,OnTouchEventHandle _handle,params object[] _params)
		{
			switch(_type)
			{
			case EnumTouchEventType.OnClick:
				if(null == onClick)
					onClick = new TouchHandle();
				onClick.SetHandle(_handle,_params);
					break;
			case EnumTouchEventType.OnDoubleClick:
				if(null == onDoubleClick)
					onDoubleClick = new TouchHandle();
				onDoubleClick.SetHandle(_handle,_params);
				break;
			case EnumTouchEventType.OnDeSelect:
				if(null == onDeSelect)
					onDeSelect = new TouchHandle();
				onDeSelect.SetHandle(_handle,_params);
				break;
			case EnumTouchEventType.OnDown:
				if(null == onDown)
					onDown = new TouchHandle();
				onDown.SetHandle(_handle,_params);
				break;
			case EnumTouchEventType.OnDrag:
				if(null == onDrag)
					onDrag = new TouchHandle();
				onDrag.SetHandle(_handle,_params);
				break;
			case EnumTouchEventType.OnDragEnd:
				if(null == onDragEnd)
					onDragEnd = new TouchHandle();
				onDragEnd.SetHandle(_handle,_params);
				break;
			case EnumTouchEventType.OnDrop:
				if(null == onDrop)
					onDrop = new TouchHandle();
				onDrop.SetHandle(_handle,_params);
				break;
			case EnumTouchEventType.OnEnter:
				if(null == onEnter)
					onEnter = new TouchHandle();
				onEnter.SetHandle(_handle,_params);
				break;
			case EnumTouchEventType.OnExit:
				if(null == onExit)
					onExit = new TouchHandle();
				onExit.SetHandle(_handle,_params);
				break;
			case EnumTouchEventType.OnMove:
				if(null == onMove)
					onMove = new TouchHandle();
				onMove.SetHandle(_handle,_params);
				break;
			case EnumTouchEventType.OnScroll:
				if(null == onScroll)
					onScroll = new TouchHandle();
				onScroll.SetHandle(_handle,_params);
				break;
			case EnumTouchEventType.OnSelect:
				if(null == onSelect)
					onSelect = new TouchHandle();
				onSelect.SetHandle(_handle,_params);
				break;
			case EnumTouchEventType.OnUp:
				if(null == onUp)
					onUp = new TouchHandle();
				onUp.SetHandle(_handle,_params);
				break;
			case EnumTouchEventType.OnUpdateSelect:
				if(null == onUpdateSelect)
					onUpdateSelect = new TouchHandle();
				onUpdateSelect.SetHandle(_handle,_params);
				break;
			}

	}
	
		void Start ()
		{
		
		}
		void Update ()
		{
		
		}
	}
}
