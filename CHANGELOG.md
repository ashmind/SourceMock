# Changelog

## [0.12.0] - 2021-11-13

### Fixed
- Fix class mock generation failure when base class has any fields
- Fix class mock generation failure when base class has internal members but no InternalsVisibleTo

## [0.11.0] - 2021-10-21

### Fixed
- Fix mock generation bug in generic types referenced through GenerateMocksForTypes

## [0.10.0] - 2021-06-21

### Changed
- All generated mocks are now internal

## [0.9.1] - 2021-06-21

### Fixed
- Fix class mocks with implicit interface implementations
- Fix class mocks with non-virtual properties

## [0.9.0] - 2021-05-03

### Added
- Initial version