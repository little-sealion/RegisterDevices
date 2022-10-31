using System;
using System.Threading.Tasks;
using RegisterDevices.Models;

namespace RegisterDevices.Services
{
    public interface IGetInventoryIdService
    {
        Task<SingleDeviceResponse> GetSingleInventoryId(string deviceId);
        Task<MultipleDeviceResponse> GetMultipleInventoryId(string[] deviceIdArray);
    }
}
