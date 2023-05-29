import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import './index.css'
import { Auth0Provider } from '@auth0/auth0-react'

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <Auth0Provider
    domain='dev-9uq4yxs3.eu.auth0.com'
    clientId='7CSFsYSuMABarIWcHj0quZYuAyxQDkgF'
    authorizationParams={{
      redirect_uri: window.location.origin,
      audience: "https://bulletinapi.no.no",
      scope: "read:posts write:posts"
    }}
    >
      <App />
    </Auth0Provider>
  </React.StrictMode>,
)
