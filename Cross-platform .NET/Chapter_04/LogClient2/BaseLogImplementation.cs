//BaseLogImplementation.cs
using System;

namespace Crossplatform.NET.Chapter04
{
    public abstract class BaseLogImplementation
    {
    	protected BaseLogImplementation(){}
    	
        public abstract void Record(string message);
    }
}
