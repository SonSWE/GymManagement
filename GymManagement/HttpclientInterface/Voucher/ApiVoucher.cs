
using ConstData;
using Newtonsoft.Json;
using ObjectInfo;
using RestSharp;
using System;
using System.Collections.Generic;

namespace GymManagement
{
    public class ApiVoucher
    {
        public static List<VoucherPackgeInfo> Search(string keySearch)
        {
            try
            {
                var client = new RestClient(CommonData.httpApiClientHost);
                var request = new RestRequest(CommonData.c_router_quan_tri + "voucher/search", Method.GET);
           
                request.AddParameter("keySearch", keySearch);

                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<VoucherPackgeInfo>>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static decimal Insert(VoucherPackgeInfo info)
        {
            try
            {
                var client = new RestClient(CommonData.httpApiClientHost);
                var request = new RestRequest(CommonData.c_router_quan_tri + "voucher/insert", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.JsonSerializer = new JsonSerializers();
                request.AddJsonBody(info);
                IRestResponse response = client.Execute(request);
                return Convert.ToDecimal(JsonConvert.DeserializeObject<ResponseInfo>(response?.Content ?? "").success);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static VoucherPackgeInfo GetDetail(string voucherCode)
        {
            try
            {
                var client = new RestClient(CommonData.httpApiClientHost);
                var request = new RestRequest(CommonData.c_router_quan_tri + "voucher/get-by-id", Method.GET);

                request.AddParameter("voucherCode", voucherCode);

                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<VoucherPackgeInfo>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static decimal Update(VoucherPackgeInfo info)
        {
            try
            {
                var client = new RestClient(CommonData.httpApiClientHost);
                var request = new RestRequest(CommonData.c_router_quan_tri + "voucher/update", Method.PUT);
                request.RequestFormat = DataFormat.Json;
                request.JsonSerializer = new JsonSerializers();
                request.AddJsonBody(info);
                //
                IRestResponse response = client.Execute(request);
                return Convert.ToDecimal(JsonConvert.DeserializeObject<ResponseInfo>(response?.Content ?? "").success);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static decimal Delete(string voucherCode)
        {
            try
            {
                var client = new RestClient(CommonData.httpApiClientHost);
                var request = new RestRequest(CommonData.c_router_quan_tri + "voucher/delete", Method.DELETE);

                request.AddParameter("voucherCode", voucherCode);
                IRestResponse response = client.Execute(request);
                return Convert.ToDecimal(JsonConvert.DeserializeObject<ResponseInfo>(response?.Content ?? "").success);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
