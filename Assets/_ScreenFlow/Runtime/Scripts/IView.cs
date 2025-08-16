namespace ScreenFlow.MVP
{
    /// <summary>
    /// Interface for views that can work with model data
    /// </summary>
    /// <typeparam name="TModel">Type of model this view can display</typeparam>
    public interface IView<TModel>
    {
        void UpdateView(TModel model);
    }
}