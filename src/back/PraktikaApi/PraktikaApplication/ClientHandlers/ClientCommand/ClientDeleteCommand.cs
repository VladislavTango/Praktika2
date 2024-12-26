﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraktikaApplication.ClientHandlers.ClientCommand
{
    public class ClientDeleteCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
