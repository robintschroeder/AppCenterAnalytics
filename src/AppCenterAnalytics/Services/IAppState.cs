using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCenterAnalytics.Services
{
    public interface IAppState
    {
        Guid GetInstallId();

        Task Init();
    }
}