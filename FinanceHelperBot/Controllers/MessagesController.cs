using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using System.Text;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Dialogs;
using System.Data.SQLite;
using Microsoft.Bot.Connector.Utilities;
using System.Data.SqlClient;
using System.Configuration;

#pragma warning disable 649
namespace FinanceHelperBot
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
        [Terms(new string[] { "Savings Account" })]
        SVBA,
        [Terms(new string[] { "Current Account" })]
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
            try
            {
                var builder = new StringBuilder();
                SqlConnection m_dbConnection;
                m_dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Dataconnection"].ToString());
                m_dbConnection.Open();
                SqlCommand command = null;
                SqlDataReader reader = null;
                string sql = null; string name = null, name1 = null;
                switch (Kind)
                {
                    case AccntOptions.SVBA:
                        sql = "select * from PROD_DETAILS where PROD_TYPE like 'AC' and PROD_NAME like 'SVBA'";
                        command = new SqlCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            name = reader["LITERATURE"].ToString();
                            name1 = reader["CHARGES"].ToString();
                        }
                        builder.AppendFormat("Benefits: {0}", name);
                        builder.AppendFormat("\nFees: {0}", name1);
                        m_dbConnection.Close();

                        break                    
                    case AccntOptions.CBA:
                        sql = "select * from PROD_DETAILS where PROD_TYPE like 'AC' and PROD_NAME like 'CBA'";
                        command = new SqlCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            name = reader["LITERATURE"].ToString();
                            name1 = reader["CHARGES"].ToString();
                        }
                        builder.AppendFormat("Benefits: {0}", name);
                        builder.AppendFormat("\nFees: {0}", name1);
                        m_dbConnection.Close();

                        break;
                    case AccntOptions.LA:
                        sql = "select * from PROD_DETAILS where PROD_TYPE like 'AC' and PROD_NAME like 'LA'";
                        command = new SqlCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            name = reader["LITERATURE"].ToString();
                            name1 = reader["CHARGES"].ToString();
                        }
                        builder.AppendFormat("Benefits: {0}", name);
                        builder.AppendFormat("\nFees: {0}", name1);
                        m_dbConnection.Close();

                        break;
                    case AccntOptions.RDA:
                        sql = "select * from PROD_DETAILS where PROD_TYPE like 'AC' and PROD_NAME like 'RDA'";
                        command = new SqlCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            name = reader["LITERATURE"].ToString();
                            name1 = reader["CHARGES"].ToString();
                        }
                        builder.AppendFormat("Benefits: {0}", name);
                        builder.AppendFormat("\nFees: {0}", name1);
                        m_dbConnection.Close();

                        break;
                    case AccntOptions.FDA:
                        sql = "select * from PROD_DETAILS where PROD_TYPE like 'AC' and PROD_NAME like 'FDA'";
                        command = new SqlCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            name = reader["LITERATURE"].ToString();
                            name1 = reader["CHARGES"].ToString();
                        }
                        builder.AppendFormat("Benefits: {0}", name);
                        builder.AppendFormat("\nFees: {0}", name1);
                        m_dbConnection.Close();

                        break;
                    case AccntOptions.Unkown:
                        builder.AppendFormat("{0},You have chosen an invalid option. I am not able to answer your query", Kind);
                        break;
                }
                return builder.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
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
            try
            {
                var builder = new StringBuilder();
                SqlConnection m_dbConnection;
                m_dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Dataconnection"].ToString());
                m_dbConnection.Open();
                SqlCommand command = null;
                SqlDataReader reader = null;
                string sql = null; string name = null, name1 = null;
                switch (Kind)
                {

                    case GPQOptions.Fuel_Card:
                        sql = "select * from PROD_DETAILS where PROD_TYPE like 'CC' and PROD_NAME like 'Fuel Card'";
                        command = new SqlCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            name = reader["LITERATURE"].ToString();
                            name1 = reader["CHARGES"].ToString();
                        }
                        builder.AppendFormat("Benefits: {0}", name);
                        builder.AppendFormat("\nFees: {0}", name1);
                        m_dbConnection.Close();

                        break;
                    case GPQOptions.Rwrd_card:
                        sql = "select * from PROD_DETAILS where PROD_TYPE like 'CC' and PROD_NAME like 'Rewards'";
                        command = new SqlCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            name = reader["LITERATURE"].ToString();
                            name1 = reader["CHARGES"].ToString();
                        }
                        builder.AppendFormat("Benefits: {0}", name);
                        builder.AppendFormat("\nFees: {0}", name1);
                        m_dbConnection.Close();

                        break;
                    case GPQOptions.Cash_Back:
                        sql = "select * from PROD_DETAILS where PROD_TYPE like 'CC' and PROD_NAME like 'Cash Back'";
                        command = new SqlCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            name = reader["LITERATURE"].ToString();
                            name1 = reader["CHARGES"].ToString();
                        }
                        builder.AppendFormat("Benefits: {0}", name);
                        builder.AppendFormat("\nFees: {0}", name1);
                        m_dbConnection.Close();

                        break;
                    case GPQOptions.Unkown:
                        builder.AppendFormat("{0},You have chosen an invalid option. I am not able to answer your query", Kind);
                        break;
                }
                return builder.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        //public static IForm<CCOrder> BuildForm()
        //{
        //    return new FormBuilder<CCOrder>()
        //            .Message("Welcome!")
        //            .Build();
        //}
    };

    [Microsoft.Bot.Connector.BotAuthentication]
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