import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44317/',
  redirectUri: baseUrl,
  clientId: 'FaceAPI_App',
  responseType: 'code',
  scope: 'offline_access FaceAPI',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'FaceAPI',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44369',
      rootNamespace: 'FaceAPI',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
