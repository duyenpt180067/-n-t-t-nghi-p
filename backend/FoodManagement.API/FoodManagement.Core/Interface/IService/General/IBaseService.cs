using FoodManagement.Core.General.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Interfaces.Service
{
    public interface IBaseService<TEntity>
    {
        #region Methods
        /// <summary>
        /// them moi duw lieu cua entity
        /// Create: PTDuyen(28/07/2021)
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>so luong hang bi sua doi</returns>
        ServiceResult AddObj(TEntity entity);
        ServiceResult AddMultiObj(List<TEntity> lstEntity);

        /// <summary>
        /// sua du lieu cua entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>so luong ban ghi bi sua</returns>
        ServiceResult UpdateObj(TEntity entity);

        /// <summary>
        /// xoa du lieu theo id
        /// </summary>
        /// <param name="entityId">Guid</param>
        /// Create: PTDuyen(28/07/2021)
        /// <returns>so ban ghi bi xoa</returns>
        ServiceResult DeleteObj(string entityId, string userName);

        /// <summary>
        /// xoa nhieu ban gi theo mang id
        /// Create: PTDuyen(03/08/2021)
        /// </summary>
        /// <param name="listEntityId">mang cac id truyen vao</param>
        /// <returns>so ban ghi bi xoa</returns>
        ServiceResult DeleteMultiObj(List<String> listEntityId, string userName);
        #endregion
    }
}
