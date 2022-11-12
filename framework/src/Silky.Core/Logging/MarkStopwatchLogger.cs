using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Silky.Core.Logging;

public class MarkStopwatchLogger
{
    public static MarkStopwatchLogger Instance = new MarkStopwatchLogger(null, true, true);
    ILogger _logger;
    Stopwatch _smark = new Stopwatch();
    string _preMarkName = "start";
    public long _allTime { get; private set; }
    string _moudleName = "UnKnowMoudleName";
    ConcurrentDictionary<string, long> _markSection = new ();
    bool _isAllowPrint = true;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="moudleName">Description such as module or class name</param>
    /// <param name="isAllowPrint">Whether to allow printing of logs</param>
    /// <param name="isMarkStart">Whether to start burying directly</param>
    public MarkStopwatchLogger(string moudleName = "", bool isAllowPrint = true, bool isMarkStart = true)
    {
        _isAllowPrint = isAllowPrint;
        _moudleName = moudleName ?? "UnKnowMoudleName";
        _logger = EngineContext.Current.Resolve<ILogger<MarkStopwatchLogger>>();
        if (isMarkStart)
            MarkStart();
    }

    public MarkStopwatchLogger MarkStart()
    {
        if (_smark == null)
            _smark = Stopwatch.StartNew();
        else
            _smark.Restart();
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mark">tagname</param>
    /// <param name="markAppendMarkSections">分段tagname not empty Append time to segment marker to aggregate</param>
    /// <param name="logLevel">log level</param>
    /// <param name="agrs"></param>
    /// <returns></returns>
    public string PrintAndMarkRestart(string mark, string[] markAppendMarkSections = null,
        LogLevel logLevel = LogLevel.Information, params object[] agrs)
    {
        var time = _smark.ElapsedMilliseconds;
        string message = string.Empty;
        if (!string.IsNullOrEmpty(mark))
        {
            _allTime += time;
            var markDes = $"{_moudleName}:[{_preMarkName}-{mark}]";
            message = $"{markDes} used time {time} ms";
            if (_isAllowPrint)
                _logger.LogWithLevel(logLevel, message, null, agrs);

            if (markAppendMarkSections != null && markAppendMarkSections.Length > 0)
                foreach (var markAppendMarkSection in markAppendMarkSections)
                {
                    _markSection.AddOrUpdate(markAppendMarkSection, time, (k, v) => v + time);
                }

            _preMarkName = mark;
        }

        _smark.Restart();
        return message;
    }

    public void PrintLogMessage(string logMessage, LogLevel logLevel = LogLevel.Information, params object[] agrs)
    {
        if (_isAllowPrint)
            _logger.LogWithLevel(logLevel, logMessage, null, agrs);
    }

    /// <summary>
    /// If not marked; use start to present time
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string PrintMarkAllTime()
    {
        var time = _allTime == 0 ? _smark.ElapsedMilliseconds : _allTime;
        var preMarkMessage = $"{_moudleName}: all time {time}ms ";
        if (_isAllowPrint)
            _logger?.LogWithLevel(LogLevel.Information, preMarkMessage, null, null);
        return preMarkMessage;
    }

    /// <summary>
    /// print segment markers
    /// </summary>
    /// <param name="markSectionName">分段tagname</param>
    /// <returns></returns>
    public string PrintMarkSection(string markSectionName)
    {
        var message = "";
        if (_markSection.TryGetValue(markSectionName, out long time))
        {
            message = $" {_moudleName}:[{markSectionName}(MarkSection)] used time amount {time}ms ";
            if (_isAllowPrint)
                _logger?.LogWithLevel(LogLevel.Information, message, null, null);
        }

        return message;
    }

    public void PrintMarkSections()
    {
        foreach (var markSectionName in _markSection.Keys)
        {
            PrintMarkSection(markSectionName);
        }
    }
}