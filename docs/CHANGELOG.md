# Changelog

## 2.0.0-preview.6
### Removed
- `ItemWithMax` and `ItemWithMin` (replaced with .NET 6 `MaxBy` and `MinBy`)

## 2.0.0-preview.5
### Removed
- MathExtensions (moved to Tavenem.Mathematics library)

## 2.0.0-preview.4
### Added
- `SelectHasValue` and `SelectNonNull` overloads without selector expressions, which operate on the sequence itself
- `ItemWithMax` and `ItemWithMin` overloads which take an `IComparer` instead of a selector function
### Changed
- Moved the `IDictionary` extension methods into the `System.Collections.Generic` namespace, so they will be available without a separate `using` statement
- Renamed the `IDictionaryExtensions` class to `TavenemIDictionaryExtensions` to avoid conflicts in the public namespace
- Moved the `IEnumerable` extension methods into the `System.Linq` namespace, renamed to `TavenemIEnumerableExtensions`
- Moved the math extension methods into the `System` namespace, renamed to `TavenemMathExtensions`
- Added a few `StringBuilder` extension methods in the `System.Text` namespace (class name `TavenemStringBuilderExtensions`)
### Removed
- Methods related to superscript and subscript numbers
- Some now-redundant overloads

## 2.0.0-preview.3
### Changed
- Make `IComparableToZero<TSelf>.NearlyZero` static

## 2.0.0-preview.1
### Added
- `IComparableToZero<TSelf>` interface, which extends `IFloatingPoint<TSelf>` and defines a `NearlyZero` value
which can be used in comparisons to avoid floating point errors
### Changed
- Update to .NET 6 preview
- Update to C# 10 preview
- Use generic math interfaces
### Removed
- Clamp overloads (now available with math interfaces)

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