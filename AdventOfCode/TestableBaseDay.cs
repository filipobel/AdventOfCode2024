using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;
public abstract class TestableBaseDay : BaseDay
{
    protected override string InputFileDirPath =>
        Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly()!.Location)!,
            base.InputFileDirPath);
}
