using Abp.Data;
using Abp.EntityFrameworkCore;
using CarPlusGo.CVAS.BPM;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.EntityFrameworkCore.Repositories
{
    public class FileUploadRepository : RentalRepositoryBase<FileUpload, long>, IFileUploadRepository
    {
        public FileUploadRepository(IDbContextProvider<RentalDbContext> dbContextProvider, IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider, transactionProvider)
        {
        }

        public async Task<int> SaveBPMFile(BPMFile bpmFile)
        {
            EnsureConnectionOpen();
            using (var command = CreateCommand("EXEC dbo.S_OASaveFile_BPM @AccountID,@RequisitionID,@FormName,@NFileName,@OFileName,@FileSize,@FlowId", CommandType.Text, 
                new SqlParameter("@AccountID",bpmFile.AccountID), 
                new SqlParameter("@RequisitionID", bpmFile.RequisitionID),
                new SqlParameter("@FormName", bpmFile.FormName),
                new SqlParameter("@NFileName", bpmFile.NFileName),
                new SqlParameter("@OFileName", bpmFile.OFileName),
                new SqlParameter("@FileSize", bpmFile.FileSize),
                new SqlParameter("@FlowId", bpmFile.FlowId)
            ))
            {
                return await command.ExecuteNonQueryAsync();
            }
        }
    }
}
