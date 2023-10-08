namespace XMLConverter.Helpers
{
	[Serializable]
	internal class XMLConveterException : Exception
	{
		public XMLConveterException() { }

        public XMLConveterException(string message) : base(message) { }

	}
}
