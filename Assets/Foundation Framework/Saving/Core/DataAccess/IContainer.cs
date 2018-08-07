using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoundationFramework
{
    public interface IContainer
    {
        void SetPath(string path);
        T Load<T>() where T : class;
        void Save<T>(T data) where T : class;
        void Delete();
    }
}
