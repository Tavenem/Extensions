# Changelog

## 3.0.1
### Updated
- Restored .NET 8 support

## 3.0.0
### Updated
- Update to .NET 10
### Removed
- `IDictionaryExtensions` - now included in .NET 10

## 2.2.0
### Updated
- Update to .NET 8

## 2.1.0
### Updated
- Update to .NET 7
### Removed
- `IComparableToZero`
- Dependency on preview features

## 2.0.0
### Added
- `SelectHasValue` and `SelectNonNull` overloads without selector expressions, which operate on the sequence itself
- `ItemWithMax` and `ItemWithMin` overloads which take an `IComparer` instead of a selector function
### Changed
- Update to .NET 6 preview
- Update to C# 10 preview
- Moved the `IDictionary` extension methods into the `System.Collections.Generic` namespace, so they will be available without a separate `using` statement
- Renamed the `IDictionaryExtensions` class to `TavenemIDictionaryExtensions` to avoid conflicts in the public namespace
- Moved the `IEnumerable` extension methods into the `System.Linq` namespace, renamed to `TavenemIEnumerableExtensions`
- Added a few `StringBuilder` extension methods in the `System.Text` namespace (class name `TavenemStringBuilderExtensions`)
- Make `IComparableToZero<TSelf>.NearlyZero` static
### Updated
- Update to release packages
### Removed
- `ItemWithMax` and `ItemWithMin` (replaced with .NET 6 `MaxBy` and `MinBy`)
- MathExtensions (moved to Tavenem.Mathematics library)
- Methods related to superscript and subscript numbers
- Some now-redundant overloads

## 1.1.0
### Added
- Add math extensions

## 1.0.4
### Fixed
- Correct NuGet links

## 1.0.3
### Fixed
- Got publish process working

## 1.0.0
### Added
- Initial release