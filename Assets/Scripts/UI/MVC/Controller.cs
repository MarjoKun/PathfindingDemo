using UnityEngine;

namespace UI
{
    [DefaultExecutionOrder(ENSURE_EXECUTION_ORDER_FOR_INITIAL_AWAKE)]
    public class Controller<TView, TModel> : MonoBehaviour where TView : View where TModel : Model<TView>
	{
		[field: SerializeField]
		protected TView CurrentView { get; private set; }
		[field: SerializeField]
		protected TModel CurrentModel { get; private set; }

        private const int ENSURE_EXECUTION_ORDER_FOR_INITIAL_AWAKE = -1;

        private void Awake ()
		{
			CurrentModel.SetView(CurrentView);
		}
	}
}