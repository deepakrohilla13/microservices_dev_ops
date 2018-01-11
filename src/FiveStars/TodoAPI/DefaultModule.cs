using Autofac;
using TodoApi;
using TodoApi.Models;
using TodoAPI.BusinessModels;

namespace TodoAPI
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<NoteRepository>().As<INoteRepository>();
            //builder.RegisterType<AlertService>().As<IAlertService>();
        }
    }
}
