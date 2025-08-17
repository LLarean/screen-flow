using System;

namespace ScreenFlow.MVP
{
    /// <summary>
    /// Example presenter for user profile screen
    /// </summary>
    public class UserProfilePresenter : IDisposable
    {
        private readonly UserProfileView _view;
        private readonly UserProfileModel _model;
        
        private bool _disposed;

        public UserProfilePresenter(UserProfileModel model, UserProfileView view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _view.OnToMenuClicked += HandleMenu;
        }

        public void Initialize()
        {
            ThrowIfDisposed();
            _view.UpdateView(_model);
        }

        private void HandleMenu()
        {
            if (_disposed) return;
        
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            
            if (disposing)
            {
                if (_view != null)
                {
                    _view.OnToMenuClicked -= HandleMenu;
                }
            }
            
            _disposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(UserProfilePresenter));
        }
    }
}