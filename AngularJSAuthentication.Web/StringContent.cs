namespace AngularJSAuthentication.Web
{
    internal class StringContent
    {
        private string stringContent;
        private object uTF8;
        private string v;

        public StringContent(string stringContent, object uTF8, string v)
        {
            this.stringContent = stringContent;
            this.uTF8 = uTF8;
            this.v = v;
        }
    }
}