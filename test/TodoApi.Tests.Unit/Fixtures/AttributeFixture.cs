using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Tests.Unit.Fixtures
{
    public class AttributeFixture<T> where T : ControllerBase
    {
        private readonly Type _type;
        private MethodInfo? _method;
        
        public AttributeFixture()
        {
            _type = typeof(T);
        }

        public void GetMethod(string method)
        {
            _method = _type.GetMethod(method);
        }
        
        public Attribute[] GetCustomAttributes()
        {
            return Attribute.GetCustomAttributes(_method);
        }

        public Attribute[] GetTCustomAttributes()
        {
            return Attribute.GetCustomAttributes(_type);
        }
    }
}