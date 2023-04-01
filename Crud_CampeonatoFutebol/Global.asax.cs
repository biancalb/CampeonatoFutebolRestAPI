using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using Crud_CampeonatoFutebol.App_Start;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Diagnostics;
using Crud_CampeonatoFutebol.Models.Services;
using Crud_CampeonatoFutebol.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Crud_CampeonatoFutebol
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IConnection connection;
        private static IModel channel;
        public const string GraphQlPath = "/graphql";
        public IApplicationBuilder app;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Use a windows service to setup queue subscriber
            //this.SetupRabbitMqSubscriber();
        }

        protected void Application_End()
        {
            channel.Close(200, "Goodbye!");
            connection.Close();
        }

        private void SetupRabbitMqSubscriber()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "TipoEvento", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var messageSplited = message.Split(';');
                var instante = messageSplited[0];

                var partidaId = messageSplited[1];
                var partida = new PartidaSvc().Find(Convert.ToInt32(partidaId));
                    
                var tipoEventoId = messageSplited[2];
                var tipoEventoNome = new TipoEventoSvc().Find(Convert.ToInt32(tipoEventoId)).NomeEvento;

                string jogadorId = string.Empty;
                Jogador jogador = null;
                string time = string.Empty;

                if (!string.IsNullOrEmpty(messageSplited[3]))
                {
                    jogadorId = messageSplited[3];
                    jogador = new JogadorSvc().Find(Convert.ToInt32(jogadorId));

                    time = new TimeSvc().Find(Convert.ToInt32(jogador.TimeAtualID)).Nome;
                }

                if (jogador == null) Debug.WriteLine($"Instante: {DateTime.Parse(instante)} - {tipoEventoNome} - {partida.TimeCampeonato.Time.Nome} x {partida.TimeCampeonato1.Time.Nome}");
                else Debug.WriteLine($"Instante: {DateTime.Parse(instante)} - {tipoEventoNome} - {time} - {jogador?.Nome}");
            };
            channel.BasicConsume(queue: "TipoEvento", true, consumer);
        }
    }
}
