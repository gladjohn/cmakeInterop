# Setup project.
PROJECT(Microsoft.Identity.Client.NativeInterop VERSION 1.0.0.0 LANGUAGES CSharp)

# Add shared library project.
ADD_LIBRARY(${PROJECT_NAME} SHARED
    "Account.cs"
    "API.cs"
    "Async.cs"
    "AuthParameters.cs"
    "AuthResult.cs"
    "Core.cs"
    "Error.cs"
    "Handle.cs"
    "Module.cs"
    "MsalRuntimeException.cs"
    "Platform.cs"
    "PropertyCollection.cs"
    "PropertyROCollection.cs"
    "ResponseStatus.cs"
    "SignOutResult.cs"
)

# nuget ref
set_property(TARGET CommonLib PROPERTY
  VS_PACKAGE_REFERENCES "NETStandard.Library_2.0.3")

# Setup installer.
INSTALL(TARGETS ${PROJECT_NAME} EXPORT ${PROJECT_NAME}Config
    ARCHIVE DESTINATION ${CMAKE_INSTALL_LIBRARY_DIR}
    LIBRARY DESTINATION ${CMAKE_INSTALL_LIBRARY_DIR}
    RUNTIME DESTINATION ${CMAKE_INSTALL_BINARY_DIR}
    INCLUDES DESTINATION ${CMAKE_INSTALL_INCLUDE_DIR}
)

# Export config.
INSTALL(EXPORT ${PROJECT_NAME}Config DESTINATION ${CMAKE_INSTALL_EXPORT_DIR})
EXPORT(TARGETS ${PROJECT_NAME} FILE ${PROJECT_NAME}Config.cmake)