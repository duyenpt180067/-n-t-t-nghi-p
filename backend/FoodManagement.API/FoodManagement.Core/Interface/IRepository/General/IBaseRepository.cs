using FoodManagement.Core.Entities.FMUser;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Interfaces.Repository
{
    public interface IBaseRepository<TEntity>
    {
        #region Methods
        /// <summary>
        /// lay tat ca cac object
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <returns>Danh sach object</returns>
        List<TEntity> GetAllObj();

        Object GetFilterPaging(JObject parameter);

        /// <summary>
        /// lay du lieu object theo id
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entityId">Guid</param>
        /// <returns>object</returns>
        TEntity GetObjById(Guid entityId);

        /// <summary>
        /// them moi duw lieu cua object
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>so luong hang bi sua doi</returns>
        string AddObj(TEntity entity, User user);

        /// <summary>
        /// sua du lieu cua object
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entity">object</param>
        /// <returns>so luong ban ghi bi sua</returns>
        string UpdateObj(TEntity entity, User user, TEntity oldObj);

        /// <summary>
        /// xoa du lieu theo id
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entityId">Guid</param>
        /// Create: PTDuyen(17/08/2021)
        /// <returns>so ban ghi bi xoa</returns>
        string DeleteObj(Guid entityId, User user, TEntity entityCheck);

        /// <summary>
        /// xoa nhieu ban gi theo mang id
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="listEntityId">mang cac id truyen vao</param>
        /// <returns>so ban ghi bi xoa</returns>
        string DeleteMultiObj(List<Guid> listEntityId, User user);

        /// <summary>
        /// thêm nhiều
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="listEntity">mang cac entity truyen vao</param>
        /// <returns>so ban ghi bi tác động</returns>
        string AddMultiObj(List<TEntity> listEntity);

        /// <summary>
        /// lay du lieu theo mot property nao do
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entity">entity truyen vao</param>
        /// <param name="property">property truyen vao</param>
        /// <returns>entity</returns>
        object GetObjByProperty(TEntity entity, PropertyInfo property);

        object GetLayoutConfig();
        string GetNewCode();
        object GetByCode(string code);
        public User GetUserByUserName(string userName);
        #endregion
    }
}
