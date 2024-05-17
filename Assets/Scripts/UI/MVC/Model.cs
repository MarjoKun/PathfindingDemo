using UnityEngine;

namespace UI
{
	public class Model<TView> : MonoBehaviour where TView : View
	{
		protected TView CurrentView { get; private set; }

		public void SetView (TView newView)
		{
			CurrentView = newView;
		}
	}
}