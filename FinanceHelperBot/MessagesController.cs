using System.Web.Http;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Data.SQLite;


#pragma warning disable 649
namespace Microsoft.Bot.Sample.PizzaBot
{
    public enum QueryOptions
    {
        Unkown,
        [Terms(new string[] { "Credit card" })]
        CCQuery,
        [Terms(new string[] { "Int Rate Query" })]
        IntRateQuery,
        [Terms(new string[] { "Account Query" })]
        ACTypeQuery,
        [Terms(new string[] { "SR Query" })]
        SR_Query
    };
    public enum GPQOptions
    {
        Unkown,
        [Terms(new string[] { "Fuel Card", "Query" })]
        Fuel_Card,
        [Terms(new string[] { "Reward Points Card", "Query" })]
        Rwrd_card,
        [Terms(new string[] { "International Travel Card", "SR Query" })]
        Cash_Back
    };
    public enum AccntOptions
    {
        Unkown,
        [Terms(new string[] { "Savings Account"})]
        SVBA,
        [Terms(new string[] { "Current Account"})]
        CBA,
        [Terms(new string[] { "Loan Account" })]
        LA,
        [Terms(new string[] { "Recurring Account" })]
        RDA,
        [Terms(new string[] { "Fixed Deposit Account" })]
        FDA


    };
    [Serializable]
    class ACCOrder
    {

        public AccntOptions Kind;
        public override string ToString()
        {
            var builder = new StringBuilder();
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=C:/Users/108216/Documents/mysqlite.db;");
            m_dbConnection.Open();
            SQLiteCommand command = null;
            SQLiteDataReader reader = null;
            string sql = null; string name = null, name1 = null;
            switch (Kind)
            {
                case AccntOptions.SVBA:
                    sql = "select * from PROD_DETAILS where PROD_TYPE='AC' and PROD_NAME='SVBA'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        name = reader["LITERATURE"].ToString();
                        name1 = reader["CHARGES"].ToString();
                    }
                    builder.AppendFormat("{0}", name);
                    builder.AppendFormat("{0}", name1);
                    m_dbConnection.Close();

                    break;
                case AccntOptions.CBA:
                    sql = "select * from PROD_DETAILS where PROD_TYPE='AC' and PROD_NAME='CBA'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        name = reader["LITERATURE"].ToString();
                        name1 = reader["CHARGES"].ToString();
                    }
                    builder.AppendFormat("{0}", name);
                    builder.AppendFormat("{0}", name1);
                    m_dbConnection.Close();

                    break;
                case AccntOptions.LA:
                    sql = "select * from PROD_DETAILS where PROD_TYPE='AC' and PROD_NAME='LA'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        name = reader["LITERATURE"].ToString();
                        name1 = reader["CHARGES"].ToString();
                    }
                    builder.AppendFormat("{0}", name);
                    builder.AppendFormat("{0}", name1);
                    m_dbConnection.Close();

                    break;
                case AccntOptions.RDA:
                    sql = "select * from PROD_DETAILS where PROD_TYPE='AC' and PROD_NAME='RDA'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        name = reader["LITERATURE"].ToString();
                        name1 = reader["CHARGES"].ToString();
                    }
                    builder.AppendFormat("{0}", name);
                    builder.AppendFormat("{0}", name1);
                    m_dbConnection.Close();

                    break;
                case AccntOptions.FDA:
                    sql = "select * from PROD_DETAILS where PROD_TYPE='AC' and PROD_NAME='FDA'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        name = reader["LITERATURE"].ToString();
                        name1 = reader["CHARGES"].ToString();
                    }
                    builder.AppendFormat("{0}", name);
                    builder.AppendFormat("{0}", name1);
                    m_dbConnection.Close();

                    break;
                case AccntOptions.Unkown:
                    builder.AppendFormat("{0},You have chosen an invalid option. I am not able to answer your query", Kind);
                    break;
            }
            return builder.ToString();
        }

    };

    [Serializable]
    class CCOrder
    {
        //        [Prompt("Hello, please choose from the options below to choose your credit card? {||}")]
        //        [Template(TemplateUsage.NotUnderstood, "What does \"{0}\" mean???")]
        //        [Describe("Kind of Query")]
        //[Optional]
        public QueryOptions Qry;
        public GPQOptions Kind;
        public override string ToString()
        {
            var builder = new StringBuilder();
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=C:/Users/108216/Documents/mysqlite.db;");
            m_dbConnection.Open();
            SQLiteCommand command = null;
            SQLiteDataReader reader = null;
            string sql = null; string name = null, name1 = null;
            switch (Kind)
            {

                case GPQOptions.Fuel_Card:
                    sql = "select * from PROD_DETAILS where PROD_TYPE='CC' and PROD_NAME='Fuel Card'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        name = reader["LITERATURE"].ToString();
                        name1 = reader["CHARGES"].ToString();
                    }
                    builder.AppendFormat("{0}", name);
                    builder.AppendFormat("{0}", name1);
                    m_dbConnection.Close();

                    break;
                case GPQOptions.Rwrd_card:
                    sql = "select * from PROD_DETAILS where PROD_TYPE='CC' and PROD_NAME='Rewards'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        name = reader["LITERATURE"].ToString();
                        name1 = reader["CHARGES"].ToString();
                    }
                    builder.AppendFormat("{0}", name);
                    builder.AppendFormat("{0}", name1);
                    m_dbConnection.Close();

                    break;
                case GPQOptions.Cash_Back:
                    sql = "select * from PROD_DETAILS where PROD_TYPE='CC' and PROD_NAME='Cash Back'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        name = reader["LITERATURE"].ToString();
                        name1 = reader["CHARGES"].ToString();
                    }
                    builder.AppendFormat("{0}", name);
                    builder.AppendFormat("{0}", name1);
                    m_dbConnection.Close();

                    break;
                case GPQOptions.Unkown:
                    builder.AppendFormat("{0},You have chosen an invalid option. I am not able to answer your query", Kind);
                    break;
            }
            return builder.ToString();
        }
        //public static IForm<CCOrder> BuildForm()
        //{
        //    return new FormBuilder<CCOrder>()
        //            .Message("Welcome!")
        //            .Build();
        //}
    };

    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private static IForm<CCOrder> BuildForm()
        {
            var builder = new FormBuilder<CCOrder>();
            ActiveDelegate<CCOrder> isCC = (ccOrder) => ccOrder.Qry == QueryOptions.CCQuery;
            System.Diagnostics.Debug.Write("Sayantan");
            System.Diagnostics.Debug.Write(isCC.ToString());
            System.Diagnostics.Debug.Write("Raha");
            //ActiveDelegate<CCOrder> isa = (ccOrder) => ccOrder.Kind == GPQOptions.Fuel_Card;
            //ActiveDelegate<CCOrder> isb = (ccOrder) => ccOrder.Kind == GPQOptions.Rwrd_card;
            //ActiveDelegate<CCOrder> isc = (ccOrder) => ccOrder.Kind == GPQOptions.Int_CC;

            return builder
                .Message("Welcome!!!")
                .Field(nameof(CCOrder.Qry))
                .Field(nameof(CCOrder.Kind), isCC)
                //.Field(nameof(CCOrder.Kind), isa)
                //.Field(nameof(CCOrder.Kind), isb)
                //.Field(nameof(CCOrder.Kind), isc)
                //.AddRemainingFields()
                .Build()
                ;

        }

        internal static IDialog<CCOrder> MakeRoot()
        {
            //int luis = 1;
            ////PizzaOrder order = null;
            //if (luis==1)
            //{
            return Chain.From(() => new PizzaOrderDialog(BuildForm));
            // }
            //else
            //{
            //    return Chain.From(() => FormDialog.FromForm(CCOrder.BuildForm))
            //                        .Do(async (context, order) =>
            //                        {
            //                            try
            //                            {
            //                                var completed = await order;
            //                                // Actually process the sandwich order...
            //                                await context.PostAsync("Query Result!"+ completed.ToString());
            //                            }
            //                            catch (FormCanceledException<CCOrder> e)
            //                            {
            //                                string reply;
            //                                if (e.InnerException == null)
            //                                {
            //                                    reply = $"You quit on {e.Last}--maybe you can finish next time!";
            //                                }
            //                                else
            //                                {
            //                                    reply = "Sorry, I've had a short circuit.  Please try again.";
            //                                }
            //                                await context.PostAsync(reply);
            //                            }
            //}); 
            // }
        }
        public async Task<Message> Post([FromBody]Message message)
        {
            return await Conversation.SendAsync(message, MakeRoot);
        }

    }
}