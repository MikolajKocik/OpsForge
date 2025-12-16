using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsForge.Domain.Exceptions;

public class CannotCancelCompletedMachineException : DomainException
{
    public CannotCancelCompletedMachineException(string message)
        : base(message) { }
}