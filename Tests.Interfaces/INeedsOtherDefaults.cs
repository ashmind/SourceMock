using System.Collections.Generic;
using System.Threading.Tasks;

namespace SourceMock.Tests.Interfaces {
    public interface INeedsOtherDefaults {
        Task ExecuteAsync();
        Task<object> GetStringAsync();
        Task<IList<int>> GetListAsync();
    }
}
