using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class Audit
    {
        
        /// <summary>
        /// 退款員工
        /// </summary>
        public string RefundEmployee { get; set; }

        /// <summary>
        /// 退款機台
        /// </summary>
        public string RefundMachine { get; set; }

        /// <summary>
        /// 交易序號
        /// </summary>
        public string TransactionNumber { get; set; }

        /// <summary>
        /// 退款時間
        /// </summary>
        public string RefundTime { get; set; }

        /// <summary>
        /// 場次時間
        /// </summary>
        public string SessionTime { get; set; }

        /// <summary>
        /// 超出時間
        /// </summary>
        public string OverTime { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// 退票方式
        /// </summary>
        public string RefundMethod { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 授權者
        /// </summary>
        public string Authorizer { get; set; }

        /// <summary>
        /// 檢查者
        /// </summary>
        public string Examiner { get; set; }

        /// <summary>
        /// 稽核明細
        /// </summary>
        public List<AuditDetail> AuditDetails { get; set; }

    }

    public class AuditDetail
    {
        /// <summary>
        /// 退票張數
        /// </summary>
        public int RefundQty { get; set; }

        /// <summary>
        /// 檢核張數
        /// </summary>
        public int ExaminQty { get; set; }

        /// <summary>
        /// 票種實體票
        /// </summary>
        public int TicketTypeActual { get; set; }

        /// <summary>
        /// 票種虛擬票
        /// </summary>
        public int TicketTypeVirtual { get; set; }

        /// <summary>
        /// 缺少實體票
        /// </summary>
        public int ShortTicketActual { get; set; }

        /// <summary>
        /// 缺少虛擬票
        /// </summary>
        public int ShortTicketVirtual { get; set; }

        /// <summary>
        /// 超出場次時間退票實體票
        /// </summary>
        public int OverTimeTicketActual { get; set; }

        /// <summary>
        /// 超出場次時間退票虛擬票
        /// </summary>
        public int OverTimeTicketVirtual { get; set; }
    }
}
