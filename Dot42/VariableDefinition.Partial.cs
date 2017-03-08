using System;

namespace Mono.Cecil.Cil
{
    /// <summary>
    /// Add extensions
    /// </summary>
    partial class VariableDefinition
    {
        private string _cachedName;

        public string CachedName
        {
            get
            {
                if(_cachedName == null)
                    throw new Exception("name not initialized.");

                return _cachedName == "<none>" ? null : _cachedName;
            }
        }

        public string GetName(MethodBody body)
        {
            if (_cachedName == null)
            {
                foreach (var scope in body.Method.DebugInformation.GetScopes())
                {
                    foreach (var variableSymbol in scope.Variables)
                        if (variableSymbol.Index == this.Index)
                        {
                            _cachedName = variableSymbol.Name;
                        }
                }

                if (_cachedName == null)
                    _cachedName = "<none>";
            }

            return CachedName;
        }
    }
}
