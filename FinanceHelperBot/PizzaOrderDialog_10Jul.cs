using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Luis.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace FinanceHelperBot
{

    [Serializable]
    public class Employee
    {
        [Describe("Employee ID")]
        [Prompt("Please enter your ID: ")]
        public string EmployeeId;
        [Describe("Secure ID")]
        [Prompt("Please enter your SECURE ID: ")]
        public string EmployeeSecId;
    }

    [Serializable]
    public class SR
    {
        [Describe("SR ID")]
        [Prompt("Please enter your Service Request Number: ")]
        public string SRId;
    }

    [LuisModel("5cbacd23-8c25-409a-84e2-e6ec7a56fdf9", "834996a542bd4faf94c301c999fc9716")]
    [Serializable]
    class PizzaOrderDialog : LuisDialog<CCOrder>
    {
        private readonly BuildFormDelegate<CCOrder> MakePizzaForm;
        Employee emp = new Employee();
        SR srreq = new SR();

        internal PizzaOrderDialog(BuildFormDelegate<CCOrder> makePizzaForm)
        {
            this.MakePizzaForm = makePizzaForm;
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry. I didn't understand you.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greetings")]
        public async Task None8(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I am good; Thank you!!!");
            context.Wait(MessageReceived);
        }

        [LuisIntent("bye")]
        public async Task None9(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Good bye, have a nice day!!!");
            context.Wait(MessageReceived);
        }
        [LuisIntent("good mood")]
        public async Task None10(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Good to hear that.");
            context.Wait(MessageReceived);
        }
        [LuisIntent("bad mood")]
        public async Task None11(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry to hear that.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Salutation")]
        public async Task None2(IDialogContext context, LuisResult result)
        {
            //changed 10-Jul
            if (emp.EmployeeId == null)
            {
                await context.PostAsync("Hello there!! How are you today! Please enter the following details before we can continue...");
                var entities = new List<EntityRecommendation>(result.Entities);
                var unlockForm = new FormDialog<Employee>(emp, BuildForm, FormOptions.PromptInStart);
                context.Call(unlockForm, FormComplete);
                //var pizzaForm = new FormDialog<CCOrder>(new CCOrder(), this.MakePizzaForm, FormOptions.PromptInStart);
                //context.Call<CCOrder>(pizzaForm, PizzaFormComplete);
            }
            else
            {
                await context.PostAsync($"Hello {emp.EmployeeId}");
                context.Wait(MessageReceived);
            }
        }

        private async Task FormComplete(IDialogContext context, IAwaitable<Employee> result)
        {
            try
            {
                var emp = await result;                            //This is where exception happens
                SqlConnection m_dbConnection;
                m_dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Dataconnection"].ToString());
                //SQLiteConnection.CreateFile("MyDatabase.sqlite");
                m_dbConnection.Open();
                string sql = "select * from Users where USER_ID=" + emp.EmployeeId + " and User_SEQID like'" + emp.EmployeeSecId + "'";
                string name = null;
                SqlCommand command = new SqlCommand(sql, m_dbConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    name = reader["User_Name"].ToString();
                if (name != null)
                {
                    await context.PostAsync($"Hello {name}. What can I help you with today?");
                    m_dbConnection.Close();
                    context.Wait(MessageReceived);
                }
                else
                {
                    await context.PostAsync($"Hello user authentication for {emp.EmployeeId} has failed. Have you entered the right credentials? Please try again by re-typing -- HELLO!!!");
                    m_dbConnection.Close();
                    context.Wait(MessageReceived);
                }

            }
            catch (FormCanceledException<Employee> e)
            {
                string reply;
                if (e.InnerException == null)
                {
                    reply = $"You quit --maybe you can finish next time!";
                }
                else
                {
                    reply = "Sorry, I've had a short circuit.  Please try again.";
                }
                await context.PostAsync(reply);
            }

            catch (Exception ex)
            {

            }
        }
        public static IForm<ACCOrder> BuildForm1()
        {
            return new FormBuilder<ACCOrder>()
                    .Message("The Following Accounts are available with out Bank!")
                    .Build();
        }
        private static IForm<Employee> BuildForm()
        {
            var builder = new FormBuilder<Employee>();
            return builder.Build();
        }
        private static IForm<SR> BuildForm3()
        {
            var builder = new FormBuilder<SR>();
            return builder.Build();
        }

        [LuisIntent("ACTypeQuery")]
        public async Task None3(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("You have asked for types of account");
            //context.Wait(MessageReceived);
            var entities = new List<EntityRecommendation>(result.Entities);
            var unlockForm = new FormDialog<ACCOrder>(new ACCOrder(), BuildForm1, FormOptions.PromptInStart);
            context.Call(unlockForm, FormComplete1);
        }
        private async Task FormComplete1(IDialogContext context, IAwaitable<ACCOrder> result)
        {
            ACCOrder order = null;
            try
            {
                order = await result;
            }
            catch (FormCanceledException<Employee> e)
            {
                string reply;
                if (e.InnerException == null)
                {
                    reply = $"You quit --maybe you can finish next time!";
                }
                else
                {
                    reply = "Sorry, I've had a short circuit.  Please try again.";
                }
                await context.PostAsync(reply);
            }
            if(order != null)
            {
                await context.PostAsync("Your Query Response: " + order.ToString());
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("CCQuery")]
        public async Task None4(IDialogContext context, LuisResult result)
        {
            var entities = new List<EntityRecommendation>(result.Entities);
            foreach (var entity in result.Entities)
            {
                string qry = null;
                System.Diagnostics.Debug.Write(entity.Type);
                switch (entity.Type)
                {
                    case "CC": qry = "1"; break;
                    case "account": qry = "ACTypeQuery"; break;
                    case "curRate": qry = "stuffed"; break;
                    default:
                        qry = "1"; break;
                }
                if (qry != null)
                {
                    entities.Add(new EntityRecommendation(type: "Qry") { Entity = qry });
                    break;
                }
            }
            var pizzaForm = new FormDialog<CCOrder>(new CCOrder(), this.MakePizzaForm, FormOptions.PromptInStart, entities);
            context.Call<CCOrder>(pizzaForm, PizzaFormComplete);
            //            System.Diagnostics.Debug.Write("2");
        }
        [LuisIntent("ShowAccount")]
        public async Task None5(IDialogContext context, LuisResult result)
        {
            try
            {
                if (emp.EmployeeId == null)
                {
                    await context.PostAsync("Please enter the following details before we can continue...");
                    var entities = new List<EntityRecommendation>(result.Entities);
                    var unlockForm2 = new FormDialog<Employee>(emp, BuildForm, FormOptions.PromptInStart);
                    context.Call(unlockForm2, FormComplete2);
                }
                else
                {
                    await context.PostAsync("Showing your account details!!! One moment Pls...");
                    SqlConnection m_dbConnection;
                    m_dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Dataconnection"].ToString());
                    //SQLiteConnection.CreateFile("MyDatabase.sqlite");
                    m_dbConnection.Open();

                    string sql1 = "select * from USERS_DETAILS where USER_ID=" + emp.EmployeeId;
                    string name0 = null, name1 = null, name2 = null;
                    SqlCommand command0 = new SqlCommand(sql1, m_dbConnection);
                    SqlDataReader reader0 = command0.ExecuteReader();
                    while (reader0.Read())
                    {
                        name0 = "Account Number: " + reader0["Account_Num"].ToString() +
                            "             Type of Account: " + reader0["Account_Type"].ToString() +
                            "             Amount of money in Account: " + reader0["Account_Bal"].ToString();
                        name1 = "Credit Card Number: " + reader0["CC_Num"].ToString() +
                            "             Type of Card Held: " + reader0["CC_TYPE"].ToString() +
                            "             Outstanding on Card: " + reader0["CC_Bal"].ToString();
                        name2 = "Loan Account Number: " + reader0["Loan_Num"].ToString() +
                            "Outstanding Loan Amount: " + reader0["Loan_Outstanding"].ToString();
                    }
                    if (name0 != null)
                    {
                        await context.PostAsync($"{name0}");
                        await context.PostAsync($"{name1}");
                        await context.PostAsync($"{name2}");
                        m_dbConnection.Close();
                        context.Wait(MessageReceived);
                    }
                    else
                    {
                        await context.PostAsync($"Unable to get details. Please try again by re-typing -- HELLO!!!");
                        m_dbConnection.Close();
                        context.Wait(MessageReceived);
                    }
                }

            }
            catch (FormCanceledException<Employee> e)
            {
                string reply;
                if (e.InnerException == null)
                {
                    reply = $"You quit --maybe you can finish next time!";
                }
                else
                {
                    reply = "Sorry, I've had a short circuit.  Please try again.";
                }
                await context.PostAsync(reply);
            }
            catch(Exception ex)
            {

            }
        }
        private async Task FormComplete2(IDialogContext context, IAwaitable<Employee> result)
        {
            try
            {
                var emp = await result;                            //This is where exception happens
                SqlConnection m_dbConnection;
                m_dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Dataconnection"].ToString());
                //SQLiteConnection.CreateFile("MyDatabase.sqlite");
                m_dbConnection.Open();
                string sql = "select * from Users where USER_ID=" + emp.EmployeeId + " and User_SEQID='" + emp.EmployeeSecId + "'";
                string name = null;
                SqlCommand command = new SqlCommand(sql, m_dbConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    name = reader["User_Name"].ToString();
                if (name != null)
                {
                    await context.PostAsync("Showing your account details!!! One moment Pls...");
                    string sql1 = "select * from USERS_DETAILS where USER_ID=" + emp.EmployeeId;
                    string name0 = null, name1 = null, name2 = null;
                    SqlCommand command0 = new SqlCommand(sql1, m_dbConnection);
                    SqlDataReader reader0 = command0.ExecuteReader();
                    while (reader0.Read())
                    {
                        name0 = "Account Number: " + reader0["Account_Num"].ToString() +
                            "             Type of Account: " + reader0["Account_Type"].ToString() +
                            "             Amount of money in Account: " + reader0["Account_Bal"].ToString();
                        name1 = "Credit Card Number: " + reader0["CC_Num"].ToString() +
                            "             Type of Card Held: " + reader0["CC_TYPE"].ToString() +
                            "             Outstanding on Card: " + reader0["CC_Bal"].ToString();
                        name2 = "Loan Account Number: " + reader0["Loan_Num"].ToString() +
                            "Outstanding Loan Amount: " + reader0["Loan_Outstanding"].ToString();
                    }
                    if (name0 != null)
                    {
                        await context.PostAsync($"{name0}");
                        await context.PostAsync($"{name1}");
                        await context.PostAsync($"{name2}");
                        m_dbConnection.Close();
                        context.Wait(MessageReceived);
                    }
                    else
                    {
                        await context.PostAsync($"Unable to get details. Please try again by re-typing -- HELLO!!!");
                        m_dbConnection.Close();
                        context.Wait(MessageReceived);
                    }
                }
                else
                {
                    await context.PostAsync($"Hello user authentication for {emp.EmployeeId} has failed. Have you entered the right credentials? Please try again by re-typing -- HELLO!!!");
                    m_dbConnection.Close();
                    context.Wait(MessageReceived);
                }

            }
            catch (FormCanceledException<Employee> e)
            {
                string reply;
                if (e.InnerException == null)
                {
                    reply = $"You quit --maybe you can finish next time!";
                }
                else
                {
                    reply = "Sorry, I've had a short circuit.  Please try again.";
                }
                await context.PostAsync(reply);
            }

            catch (Exception ex)
            {

            }
        }


        [LuisIntent("ShowSRStatus")]
        public async Task None6(IDialogContext context, LuisResult result)
        {
            srreq.SRId = null;
            var unlockForm = new FormDialog<SR>(srreq, BuildForm3, FormOptions.PromptInStart);
            context.Call(unlockForm, FormComplete3);
            //context.Wait(MessageReceived);
        }
        private async Task FormComplete3(IDialogContext context, IAwaitable<SR> result)
        {
            try
            {
                var srreq = await result;                            //This is where exception happens
                SqlConnection m_dbConnection;
                m_dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Dataconnection"].ToString());
                //SQLiteConnection.CreateFile("MyDatabase.sqlite");
                m_dbConnection.Open();
                string sql = "select * from SR_DETAILS where SR_ID=" + srreq.SRId;
                string name = null;
                SqlCommand command = new SqlCommand(sql, m_dbConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    name = reader["SR_ID"].ToString();
                m_dbConnection.Close();
                if (name != null)
                {
                    await context.PostAsync("Showing your requested SR details!!! One moment Pls...");
                    string sql1 = "select * from SR_DETAILS where SR_ID=" + srreq.SRId;
                    string name0 = null, name1 = null, name2 = null, name3 = null,name4=null ;
                    m_dbConnection.Open();
                    SqlCommand command0 = new SqlCommand(sql1, m_dbConnection);
                    SqlDataReader reader0 = command0.ExecuteReader();
                    while (reader0.Read())
                    {
                        name0 = "SR Number: " + reader0["SR_ID"].ToString();
                        name1 = "Summary: " + reader0["SR_SUMMARY"].ToString();
                        name2 = "SR Status: " + reader0["SR_Status"].ToString();
                        name3 = "Nxt Date of Action:" + reader0["Next_Action_Date"].ToString();
                        name4 = "Nxt Date of Action:" + reader0["Pending_WITH"].ToString();
                    }
                    if (name0 != null)
                    {
                        await context.PostAsync($"{name0}");
                        await context.PostAsync($"{name1}");
                        await context.PostAsync($"{name2}");
                       // await context.PostAsync($"{name3}");
                        await context.PostAsync($"{name4}");

                        m_dbConnection.Close();
                        context.Wait(MessageReceived);
                    }
                    else
                    {
                        await context.PostAsync($"Unable to get details. Please try again later!!!");
                        m_dbConnection.Close();
                        context.Wait(MessageReceived);
                    }
                }
                else
                {
                    await context.PostAsync($"Hello SR ID entered: {srreq.SRId}. Have you entered the right details? Please check and try again by re-typing -- SR!!!");
                    m_dbConnection.Close();
                    context.Wait(MessageReceived);
                }

            }
            catch (FormCanceledException<Employee> e)
            {
                string reply;
                if (e.InnerException == null)
                {
                    reply = $"You quit --maybe you can finish next time!";
                }
                else
                {
                    reply = "Sorry, I've had a short circuit.  Please try again.";
                }
                await context.PostAsync(reply);
            }

            catch (Exception ex)
            {

            }
        }

        //changed 10-Jul
        //[LuisIntent("SavRate")]
        //[LuisIntent("LoanRate")]
        [LuisIntent("CurRate")]
        public async Task None7(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Showing interest rate details....");
            await context.PostAsync("Savings Account Interest Rates: 4% PA");
            await context.PostAsync("Current Account Interest Rates: 0.5% PA");
            await context.PostAsync("Loan interest rate for Loans <10 Yrs: 9%");
            await context.PostAsync("Loan interest rate for Loans >10 Yrs: 11%");
            await context.PostAsync("Deposit interest rate for Tenure >10 Yrs: 7.5%");
            await context.PostAsync("Deposit interest rate for Tenure <10 Yrs: 8.5%");
            await context.PostAsync("Crediit Card interest rate for 10% PM");
            context.Wait(MessageReceived);
        }

        private async Task PizzaFormComplete(IDialogContext context, IAwaitable<CCOrder> result)
        {
            CCOrder order = null;
            try
            {
                order = await result;
            }
            catch (OperationCanceledException)
            {
                await context.PostAsync("You canceled the form!");
                return;
            }

            if (order != null)
            {
                await context.PostAsync("Your Query Response: ");
                await context.PostAsync(order.ToString());
            }
            else
            {
                await context.PostAsync("Form returned empty response!");
            }

            context.Wait(MessageReceived);
        }

    }
}