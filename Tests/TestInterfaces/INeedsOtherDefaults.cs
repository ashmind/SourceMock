using System.Collections.Generic;
using System.Threading.Tasks;

namespace SourceMock.Tests.TestInterfaces {
    public interface INeedsOtherDefaults {
        Task ExecuteAsync();
        Task<object> GetStringAsync();
        Task<IList<int>> GetListAsync();
    }
}
