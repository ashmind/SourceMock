namespace SourceMock.Tests.Interfaces {
    public abstract class AbstractClass {
        private readonly string? _getStringDefault;

        public AbstractClass(string getStringDefault) {
            _getStringDefault = getStringDefault;
        }

        protected AbstractClass() {
        }

        public void NonVirtual() {}
        private void Private() {}
        protected virtual void Protected() {}
        protected internal virtual void ProtectedInternal() {}
        private protected virtual void PrivateProtected() {}

        public abstract int Get();
        public virtual string? GetString() => _getStringDefault;
    }
}
