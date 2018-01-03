using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TodoApi;
using TodoApi.Models;
using System.Net;

namespace TodoAPI.Controllers
{
    [Route("api/System")]
    public class SystemController : Controller
    {
        private readonly INoteRepository _noteRepository;
        
        public SystemController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            var hostname = string.Empty;
            var machineIP = string.Empty;

            if(!string.IsNullOrEmpty(Dns.GetHostName()))
            {
                machineIP = System.Environment.NewLine;
                hostname = Dns.GetHostName();
                var address = Dns.GetHostEntry(hostname);
                if(address != null && address.AddressList!=null && address.AddressList.Length>=0)
                {
                    machineIP = String.Join<System.Net.IPAddress>(System.Environment.NewLine,address.AddressList);
                }
            }

            var machineDetails = hostname+" "+machineIP;

            if (setting == "init")
            {
                _noteRepository.RemoveAllNotes();
                _noteRepository.AddNote(new Note() { Id = "1", Body = "Test note 1 "+machineDetails, 
                            CreatedOn = DateTime.Now, 
                            UpdatedOn = DateTime.Now, UserId = 1 });
                _noteRepository.AddNote(new Note() { Id = "2", Body = "Test note 2 "+machineDetails, 
                            CreatedOn = DateTime.Now, 
                            UpdatedOn = DateTime.Now, UserId = 1 });
                _noteRepository.AddNote(new Note() { Id = "3", Body = "Test note 3 "+machineDetails, 
                            CreatedOn = DateTime.Now, 
                            UpdatedOn = DateTime.Now, UserId = 2 });
                _noteRepository.AddNote(new Note() { Id = "4", Body = "Test note 4 "+machineDetails, 
                            CreatedOn = DateTime.Now, 
                            UpdatedOn = DateTime.Now, UserId = 2 });

                return "Done from  "+machineDetails;
            }

            return "Unknown from "+machineDetails;
        }
    }
}