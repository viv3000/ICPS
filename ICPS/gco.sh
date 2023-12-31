rm ICPS.csproj

echo '<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <ProjectGuid>{9468B8FC-2A40-4653-9065-B0B854B1DAAF}</ProjectGuid>
    <OutputType>Exe</OutputType>
    <RootNamespace>ICPS</RootNamespace>
    <AssemblyName>ICPS</AssemblyName>
    <TargetFrameworkVersion>v4.7</TargetFrameworkVersion>
  </PropertyGroup>
  <PropertyGroup Condition=" '"'\$(Configuration)|\$(Platform)' == 'Debug|x86'"' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug</OutputPath>
    <DefineConstants>DEBUG;</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <PlatformTarget>x86</PlatformTarget>
  </PropertyGroup>
  <PropertyGroup Condition=" '"'\$(Configuration)|\$(Platform)' == 'Release|x86'"' ">
    <Optimize>true</Optimize>
    <OutputPath>bin\Release</OutputPath>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <PlatformTarget>x86</PlatformTarget>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System.Windows.Forms" />
    <Reference Include="System.Data" />
    <Reference Include="mscorlib" />
    <Reference Include="System.Numerics" />
    <Reference Include="System" />
    <Reference Include="System.Transactions" />
    <Reference Include="System.Drawing" />
    <Reference Include="System.Buffers">
      <HintPath>..\packages\System.Buffers.4.5.1\lib\net461\System.Buffers.dll</HintPath>
    </Reference>
    <Reference Include="System.Numerics.Vectors">
      <HintPath>..\packages\System.Numerics.Vectors.4.5.0\lib\net46\System.Numerics.Vectors.dll</HintPath>
    </Reference>
    <Reference Include="System.Runtime.CompilerServices.Unsafe">
      <HintPath>..\packages\System.Runtime.CompilerServices.Unsafe.6.0.0\lib\net461\System.Runtime.CompilerServices.Unsafe.dll</HintPath>
    </Reference>
    <Reference Include="System.Memory">
      <HintPath>..\packages\System.Memory.4.5.5\lib\net461\System.Memory.dll</HintPath>
    </Reference>
    <Reference Include="System.Diagnostics.DiagnosticSource">
      <HintPath>..\packages\System.Diagnostics.DiagnosticSource.6.0.0\lib\net461\System.Diagnostics.DiagnosticSource.dll</HintPath>
    </Reference>
    <Reference Include="System.Threading.Tasks.Extensions">
      <HintPath>..\packages\System.Threading.Tasks.Extensions.4.5.4\lib\net461\System.Threading.Tasks.Extensions.dll</HintPath>
    </Reference>
    <Reference Include="MySqlConnector">
      <HintPath>..\packages\MySqlConnector.2.2.7\lib\net461\MySqlConnector.dll</HintPath>
    </Reference>
  </ItemGroup>
  <ItemGroup>
    <None Include="packages.config" />
  </ItemGroup>
' >> ICPS.csproj;

files=$(find . -name "*" -type f -exec ls  {} \; | grep ".cs" | grep -v ".csp" | grep -v "./bin" | grep -v "./obj"| grep -v "./packages")

echo "  <ItemGroup>" >> ICPS.csproj
for file in $files;
do
	echo '    <Compile Include="'$1''$file'" />'  | sed -e "s/\.[/]//" >> ICPS.csproj;
done;
echo "  </ItemGroup>" >> ICPS.csproj;


dirs=$(find . -type d | sed -e "s/\.[/]//" | grep -v "\." | grep -v "bin" | grep -v "obj"); 

echo "  <ItemGroup>" >> ICPS.csproj;
for dir in $dirs; 
do
	echo '    <Folder Include="'$1''$dir'" />' >> ICPS.csproj;
done;
echo "  </ItemGroup>" >> ICPS.csproj;

echo '<Import Project="$(MSBuildBinPath)\Microsoft.CSharp.targets" />
</Project>' >> ICPS.csproj

