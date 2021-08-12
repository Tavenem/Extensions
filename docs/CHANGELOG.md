# Changelog

## 2.0.0-preview.2
### Changed
- Remove required setter for `IComparableToZero<TSelf>.NearlyZero`

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