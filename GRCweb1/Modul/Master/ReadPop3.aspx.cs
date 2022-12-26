using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Limilabs.Mail;
using Limilabs.Client.POP3;
using Limilabs.Client.IMAP;


namespace GRCweb1.Modul.Master
{
    public partial class ReadPop3 : System.Web.UI.Page
    {
        internal class LastRun
        {
            public long UIDValidtiy { get; set; }
            public long LargestUID { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LastRun last = LoadPreviousRun();

            //Pop3 pop3 = new Pop3();
            //pop3.ConnectSSL("mail.grcboard.com");
            //pop3.UseBestLogin("luyiko@grcboard.com","1234567");

            //MailBuilder builder = new MailBuilder();
            //int aa = 0;
            //foreach (string uid in pop3.GetAll())
            //{
            //    IMail email = builder.CreateFromEml(pop3.GetMessageByUID(uid));

            //    //Console.WriteLine(email.Subject);
            //    //Console.WriteLine(email.Text);
            //    aa++;
            //    if (aa >= 10)
            //        break;

            //    TextArea1.InnerText += email.Subject;
            //}
            //pop3.Close();

            Main2();
        }
        private void SaveLargestUID(long uid)
        {
            // Your code that saves the uid.
        }

        private long LoadLargestFromPreviousRun()
        {
            long test = 0;

            return test;
            // Your code that loads the largest uid (null on the first run).
        }
        private void Main2()
        {
            using (Imap client = new Imap())
            {
                client.ConnectSSL("mail.grcboard.com"); // or ConnectSSL for SSL
                client.UseBestLogin("luyiko@grcboard.com", "1234567");

                FolderStatus status = client.SelectInbox();

                List<long> uids = client.GetAll();
                uids.Reverse();
                List<long> newest40 = uids.Take(10).ToList();

                foreach (long uid in uids)
                {
                    var eml = client.GetMessageByUID(uid);
                    IMail email = new MailBuilder().CreateFromEml(eml);

                    TextArea2.InnerText += email.Subject + ", ";
                    //Console.WriteLine(email.Subject);
                    //Console.WriteLine(email.Text);

                }

                client.Close();
            }

        }
        private static void Main()
        {
            //LastRun last = LoadPreviousRun();
            long? largestUID = 1;     //LoadLargestFromPreviousRun();

            LastRun last = new LastRun();
            last.UIDValidtiy = 0;

            using (Imap imap = new Imap())
            {
                imap.ConnectSSL("mail.grcboard.com"); // or ConnectSSL for SSL
                imap.UseBestLogin("luyiko@grcboard.com", "1234567");

                FolderStatus status = imap.SelectInbox();

                List<long> uids;
                if (last == null || last.UIDValidtiy != status.UIDValidity)
                {
                    uids = imap.GetAll();
                }
                else
                {
                    uids = imap.Search().Where(
                        Expression.UID(Range.From(last.LargestUID)));

                    //uids.Remove(largestUID);
                }

                foreach (long uid in uids)
                {
                    var eml = imap.GetMessageByUID(uid);
                    IMail email = new MailBuilder()
                        .CreateFromEml(eml);

                    Console.WriteLine(email.Subject);
                    Console.WriteLine(email.Text);

                    LastRun current = new LastRun
                    {
                        UIDValidtiy = status.UIDValidity,
                        LargestUID = uid
                    };

                    //SaveThisRun(current);
                }
                imap.Close();
            }
        }
        // C#

        private void SaveThisRun(LastRun run)
        {
            // Your code that saves run data.
        }

        private LastRun LoadPreviousRun()
        {
            LastRun lastRun = null;
            // Your code that loads last run data (null on the first run).

            return lastRun;
        }

    }
}