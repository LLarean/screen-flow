using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace ScreenFlow
{
    public class ViewContainerTests
    {
        private GameObject _containerGameObject;
        private ViewContainer _viewContainer;
        private Canvas _canvas;
        private TestView _testViewPrefab;

        [SetUp]
        public void Setup()
        {
            var canvasGameObject = new GameObject("TestCanvas");
            _canvas = canvasGameObject.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            _containerGameObject = new GameObject("ViewContainer");
            _viewContainer = _containerGameObject.AddComponent<ViewContainer>();
        
            var canvasField = typeof(ViewContainer).GetField("_canvas", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            canvasField?.SetValue(_viewContainer, _canvas);

            CreateTestViewPrefab();
        
            var prefabsField = typeof(ViewContainer).GetField("_viewsPrefabs", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var prefabsList = (System.Collections.Generic.List<View>)prefabsField?.GetValue(_viewContainer);
            prefabsList?.Add(_testViewPrefab);
        }

        [TearDown]
        public void TearDown()
        {
            if (_containerGameObject != null)
                Object.DestroyImmediate(_containerGameObject);
        
            if (_canvas != null)
                Object.DestroyImmediate(_canvas.gameObject);
            
            if (_testViewPrefab != null)
                Object.DestroyImmediate(_testViewPrefab.gameObject);
        }

        private void CreateTestViewPrefab()
        {
            var prefabGameObject = new GameObject("TestView");
            prefabGameObject.AddComponent<RectTransform>();
            _testViewPrefab = prefabGameObject.AddComponent<TestView>();
        }

        [Test]
        public void Get_WhenViewDoesNotExist_CreatesNewView()
        {
            // Act
            var result = _viewContainer.Get<TestView>();
            var result2 = _viewContainer.Get<TestView>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<TestView>(result);
        }

        [Test]
        public void Get_WhenViewAlreadyExists_ReturnsExistingView()
        {
            // Arrange
            var firstView = _viewContainer.Get<TestView>();

            // Act
            var secondView = _viewContainer.Get<TestView>();

            // Assert
            Assert.AreSame(firstView, secondView);
        }

        [Test]
        public void Get_WhenPrefabNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            var prefabsField = typeof(ViewContainer).GetField("_viewsPrefabs", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var prefabsList = (System.Collections.Generic.List<View>)prefabsField?.GetValue(_viewContainer);
            prefabsList?.Clear();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _viewContainer.Get<TestView>());
            Assert.That(exception.Message, Does.Contain("Failed to create view of type TestView"));
        }

        [Test]
        public void HideAll_HidesAllViewsOnScene()
        {
            // Arrange
            var view1 = _viewContainer.Get<TestView>();
            var view2 = _viewContainer.Get<TestView>();
        
            view1.Show(); // Показываем view

            // Act
            _viewContainer.HideAll();

            // Assert
            Assert.IsFalse(view1.IsVisible);
        }

        [UnityTest]
        public IEnumerator Get_CreatedViewHasCorrectTransformSetup()
        {
            // Act
            var view = _viewContainer.Get<TestView>();
        
            yield return null;

            // Assert
            var rectTransform = view.GetComponent<RectTransform>();
            Assert.AreEqual(Vector3.one, rectTransform.localScale);
            Assert.AreEqual(Vector3.zero, rectTransform.localPosition);
            Assert.AreEqual(Quaternion.identity, rectTransform.localRotation);
            Assert.AreEqual(Vector2.zero, rectTransform.anchorMin);
            Assert.AreEqual(Vector2.one, rectTransform.anchorMax);
        }
    }

    public class TestView : View
    {
        public bool IsVisible { get; private set; }

        public override void Show()
        {
            base.Show();
            IsVisible = true;
        }

        public override void Hide()
        {
            base.Hide();
            IsVisible = false;
        }
    }
}