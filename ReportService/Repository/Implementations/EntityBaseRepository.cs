using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using ReportService.Context;
using ReportService.Entity;
using ReportService.Repository.Interfaces;

namespace ReportService.Repository.Implementations
{
  public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T: EntityBase, new()
  {
    //private readonly AppDbContext _context;
    private readonly XmlDocument _xmlDocument;
    private readonly IWebHostEnvironment _env;
    private string _dbPath = "\\ReportDb.xml";
    private readonly string _fullPath;
    private readonly DataSet _xmlDataSet;

    public EntityBaseRepository(IWebHostEnvironment env)
    {
      //_context = context;
      _env = env;
      _xmlDocument = new XmlDocument();
      _fullPath = string.Concat(_env.WebRootPath, _dbPath);
      _xmlDataSet = new DataSet();
      _xmlDataSet.ReadXml(_fullPath);
      //InitializeXmlDb();
    }

    private void InitializeXmlDb()
    {
      DataTable dataTable = null;
      if (!_xmlDataSet.Tables.Any())
      {
        dataTable = new DataTable("Report");
        dataTable.Columns.Add(nameof(Report.Id), typeof(int));
        dataTable.Columns.Add(nameof(Report.CalorieExpenditure), typeof(double));
        dataTable.Columns.Add(nameof(Report.CalorieIntake), typeof(double));
        dataTable.Columns.Add(nameof(Report.IsSurplus), typeof(bool));
        dataTable.Columns.Add(nameof(Report.Created), typeof(DateTime));
        _xmlDataSet.Tables.Add(dataTable);
        _xmlDataSet.WriteXml(_fullPath);
      }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
      //var entities = await _context.Set<T>().ToListAsync();
      var entities = new List<T>();
      return entities;
    }

    public async Task<T> GetByIdAsync(int id)
    {
      //var entity = await _context.Set<T>().FirstOrDefaultAsync(en => en.Id == id);
      var entity = new T();
      return entity;
    }

    public async Task<IEnumerable<T>> GetEntitiesAsync(Expression<Func<T, bool>> expression)
    {
      //var results = await _context.Set<T>().Where(expression).ToListAsync();
      var entities = new List<T>();
      return entities;
    }

    public async Task<T> AddAsync(T entity)
    {
      Report report = entity as Report;
      var dataSet = new DataSet();
      dataSet.ReadXml(_fullPath);
      if (!dataSet.Tables.Any())
      {
        InitializeTable(dataSet);
      }
      DataTable dataTable = dataSet.Tables[0];

      if (!Exists(dataSet, report.Created))
      {
        var newId = dataTable.Rows.Count + 1;
        dataTable.Rows.Add(newId, report.CalorieExpenditure, report.CalorieIntake, report.IsSurplus, report.Created);
        dataSet.WriteXml(_fullPath);
        return report as T;
      }
      await UpdateAsync(report.Id, entity, dataSet);

      return report as T;
      //var entry = await _context.Set<T>().AddAsync(entity);
      //await _context.SaveChangesAsync();
      //return entry.Entity;
    }

    private void InitializeTable(DataSet dataSet)
    {
      var dataTable = new DataTable("Report");
      dataTable.Columns.Add(nameof(Report.Id), typeof(int));
      dataTable.Columns.Add(nameof(Report.CalorieExpenditure), typeof(double));
      dataTable.Columns.Add(nameof(Report.CalorieIntake), typeof(double));
      dataTable.Columns.Add(nameof(Report.IsSurplus), typeof(bool));
      dataTable.Columns.Add(nameof(Report.Created), typeof(DateTime));
      dataSet.Tables.Add(dataTable);
      dataSet.WriteXml(_fullPath);
    }

    public async Task UpdateAsync(int id, T entity, DataSet dataset)
    {
      Report report = entity as Report;
      DataRow selectedRow = null;
      DataTable table = dataset.Tables[0];
      for (int i = 0; i < table.Rows.Count; i++)
      {
        var row = table.Rows[i];
        if (DateTime.TryParse(row["Created"].ToString(), out var existingDate) && existingDate == report.Created)
        {
          selectedRow = row;
          break;
        }
      }

      if (selectedRow != null)
      {
        selectedRow.BeginEdit();
        selectedRow["CalorieExpenditure"] = report.CalorieExpenditure;
        selectedRow["CalorieIntake"] = report.CalorieIntake;
        selectedRow.EndEdit();
      }

      dataset.WriteXml(_fullPath);
    }

    public async Task DeleteAsync(int id)
    {
      //var entity = await _context.Set<T>().FirstOrDefaultAsync(en => en.Id == id);
      //EntityEntry entry = _context.Entry(entity);
      //entry.State = EntityState.Deleted;
      //await _context.SaveChangesAsync();
    }

    private bool Exists(DataSet dataset, DateTime created)
    {
      var table = dataset.Tables[0];
      for (int i = 0; i < table.Rows.Count; i++)
      {
        var row = table.Rows[i];
        if (DateTime.TryParse(row["Created"].ToString(), out var existingId) && existingId == created)
        {
          return true;
        }
      }

      return false;
    }
  }
}
