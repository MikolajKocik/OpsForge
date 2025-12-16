using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsForge.Domain.Exceptions;

public class AlreadyCompletedMachineException : DomainException
{
    public AlreadyCompletedMachineException(string message)
        : base(message) { }
}