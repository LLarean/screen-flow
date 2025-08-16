using System;

namespace ScreenFlow.MVP
{
    /// <summary>
    /// Base interface for all presenters
    /// </summary>
    public interface IPresenter : IDisposable
    {
    }

    /// <summary>
    /// Interface for presenters that work with views but without models
    /// </summary>
    /// <typeparam name="TView">Type of view this presenter controls</typeparam>
    public interface IPresenter<TView> : IPresenter where TView : View
    {
        void SetView(TView view);
        void Initialize();
    }

    /// <summary>
    /// Interface for presenters that work with both views and models
    /// </summary>
    /// <typeparam name="TView">Type of view this presenter controls</typeparam>
    /// <typeparam name="TModel">Type of model this presenter works with</typeparam>
    public interface IPresenter<TView, TModel> : IPresenter where TView : View
    {
        void SetView(TView view);
        void Initialize(TModel model);
    }
}