//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.17.0.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

namespace APICoreTemplate.Dashboard.ClientApi {

    export interface IDBClient {

        checkConnection(): Promise<FileResponse | null>;
    }

    export class DBClient implements IDBClient {
        private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
        private baseUrl: string;
        protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

        constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
            this.http = http ? http : window as any;
            this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "https://localhost:44370";
        }

        checkConnection(): Promise<FileResponse | null> {
            let url_ = this.baseUrl + "/api/v1/DB";
            url_ = url_.replace(/[?&]$/, "");

            let options_: RequestInit = {
                method: "GET",
                headers: {
                    "Accept": "application/octet-stream"
                }
            };

            return this.http.fetch(url_, options_).then((_response: Response) => {
                return this.processCheckConnection(_response);
            });
        }

        protected processCheckConnection(response: Response): Promise<FileResponse | null> {
            const status = response.status;
            let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
            if (status === 200 || status === 206) {
                const contentDisposition = response.headers ? response.headers.get("content-disposition") : undefined;
                let fileNameMatch = contentDisposition ? /filename\*=(?:(\\?['"])(.*?)\1|(?:[^\s]+'.*?')?([^;\n]*))/g.exec(contentDisposition) : undefined;
                let fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[3] || fileNameMatch[2] : undefined;
                if (fileName) {
                    fileName = decodeURIComponent(fileName);
                } else {
                    fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
                    fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
                }
                return response.blob().then(blob => { return { fileName: fileName, data: blob, status: status, headers: _headers }; });
            } else if (status !== 200 && status !== 204) {
                return response.text().then((_responseText) => {
                    return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                });
            }
            return Promise.resolve<FileResponse | null>(null as any);
        }
    }

    export interface IUserClient {

        getAll(pageNumber: number | null | undefined, pageSize: number | null | undefined): Promise<UsersDetailsModel[]>;

        getAllBySearch(columnName: string | null | undefined, searchValue: string | null | undefined, pageNumber: number | null | undefined, pageSize: number | null | undefined): Promise<UsersDetailsModel[]>;

        getTotalCount(): Promise<number>;

        getTotalCountBySearch(columnName: string | null | undefined, searchValue: string | null | undefined): Promise<number>;

        get(id: number | undefined): Promise<UsersDetailsModel>;

        getUserByEmail(userEmail: string | null | undefined): Promise<UsersDetailsModel>;

        registerUser(user: UserCreateModel): Promise<number>;

        update(id: number | undefined, roles: string | null | undefined): Promise<number>;

        getHistoryTotalCount(id: number | undefined): Promise<number>;

        getHistory(id: number | undefined, pageNumber: number | undefined, pageSize: number | undefined): Promise<UserHistoryModel[]>;
    }

    export class UserClient implements IUserClient {
        private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
        private baseUrl: string;
        protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

        constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
            this.http = http ? http : window as any;
            this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "https://localhost:44370";
        }

        getAll(pageNumber: number | null | undefined, pageSize: number | null | undefined): Promise<UsersDetailsModel[]> {
            let url_ = this.baseUrl + "/api/v1/User/GetAllAsync?";
            if (pageNumber !== undefined && pageNumber !== null)
                url_ += "pageNumber=" + encodeURIComponent("" + pageNumber) + "&";
            if (pageSize !== undefined && pageSize !== null)
                url_ += "pageSize=" + encodeURIComponent("" + pageSize) + "&";
            url_ = url_.replace(/[?&]$/, "");

            let options_: RequestInit = {
                method: "GET",
                headers: {
                    "Accept": "application/json"
                }
            };

            return this.http.fetch(url_, options_).then((_response: Response) => {
                return this.processGetAll(_response);
            });
        }

        protected processGetAll(response: Response): Promise<UsersDetailsModel[]> {
            const status = response.status;
            let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
            if (status === 200) {
                return response.text().then((_responseText) => {
                    let result200: any = null;
                    let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                    if (Array.isArray(resultData200)) {
                        result200 = [] as any;
                        for (let item of resultData200)
                            result200!.push(UsersDetailsModel.fromJS(item));
                    }
                    else {
                        result200 = <any>null;
                    }
                    return result200;
                });
            } else if (status !== 200 && status !== 204) {
                return response.text().then((_responseText) => {
                    return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                });
            }
            return Promise.resolve<UsersDetailsModel[]>(null as any);
        }

        getAllBySearch(columnName: string | null | undefined, searchValue: string | null | undefined, pageNumber: number | null | undefined, pageSize: number | null | undefined): Promise<UsersDetailsModel[]> {
            let url_ = this.baseUrl + "/api/v1/User/GetAllBySearchAsync?";
            if (columnName !== undefined && columnName !== null)
                url_ += "columnName=" + encodeURIComponent("" + columnName) + "&";
            if (searchValue !== undefined && searchValue !== null)
                url_ += "searchValue=" + encodeURIComponent("" + searchValue) + "&";
            if (pageNumber !== undefined && pageNumber !== null)
                url_ += "pageNumber=" + encodeURIComponent("" + pageNumber) + "&";
            if (pageSize !== undefined && pageSize !== null)
                url_ += "pageSize=" + encodeURIComponent("" + pageSize) + "&";
            url_ = url_.replace(/[?&]$/, "");

            let options_: RequestInit = {
                method: "GET",
                headers: {
                    "Accept": "application/json"
                }
            };

            return this.http.fetch(url_, options_).then((_response: Response) => {
                return this.processGetAllBySearch(_response);
            });
        }

        protected processGetAllBySearch(response: Response): Promise<UsersDetailsModel[]> {
            const status = response.status;
            let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
            if (status === 200) {
                return response.text().then((_responseText) => {
                    let result200: any = null;
                    let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                    if (Array.isArray(resultData200)) {
                        result200 = [] as any;
                        for (let item of resultData200)
                            result200!.push(UsersDetailsModel.fromJS(item));
                    }
                    else {
                        result200 = <any>null;
                    }
                    return result200;
                });
            } else if (status !== 200 && status !== 204) {
                return response.text().then((_responseText) => {
                    return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                });
            }
            return Promise.resolve<UsersDetailsModel[]>(null as any);
        }

        getTotalCount(): Promise<number> {
            let url_ = this.baseUrl + "/api/v1/User/GetTotalCountAsync";
            url_ = url_.replace(/[?&]$/, "");

            let options_: RequestInit = {
                method: "GET",
                headers: {
                    "Accept": "application/json"
                }
            };

            return this.http.fetch(url_, options_).then((_response: Response) => {
                return this.processGetTotalCount(_response);
            });
        }

        protected processGetTotalCount(response: Response): Promise<number> {
            const status = response.status;
            let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
            if (status === 200) {
                return response.text().then((_responseText) => {
                    let result200: any = null;
                    let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                    result200 = resultData200 !== undefined ? resultData200 : <any>null;

                    return result200;
                });
            } else if (status !== 200 && status !== 204) {
                return response.text().then((_responseText) => {
                    return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                });
            }
            return Promise.resolve<number>(null as any);
        }

        getTotalCountBySearch(columnName: string | null | undefined, searchValue: string | null | undefined): Promise<number> {
            let url_ = this.baseUrl + "/api/v1/User/GetTotalCountBySearchAsync?";
            if (columnName !== undefined && columnName !== null)
                url_ += "columnName=" + encodeURIComponent("" + columnName) + "&";
            if (searchValue !== undefined && searchValue !== null)
                url_ += "searchValue=" + encodeURIComponent("" + searchValue) + "&";
            url_ = url_.replace(/[?&]$/, "");

            let options_: RequestInit = {
                method: "GET",
                headers: {
                    "Accept": "application/json"
                }
            };

            return this.http.fetch(url_, options_).then((_response: Response) => {
                return this.processGetTotalCountBySearch(_response);
            });
        }

        protected processGetTotalCountBySearch(response: Response): Promise<number> {
            const status = response.status;
            let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
            if (status === 200) {
                return response.text().then((_responseText) => {
                    let result200: any = null;
                    let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                    result200 = resultData200 !== undefined ? resultData200 : <any>null;

                    return result200;
                });
            } else if (status !== 200 && status !== 204) {
                return response.text().then((_responseText) => {
                    return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                });
            }
            return Promise.resolve<number>(null as any);
        }

        get(id: number | undefined): Promise<UsersDetailsModel> {
            let url_ = this.baseUrl + "/api/v1/User/GetAsync?";
            if (id === null)
                throw new Error("The parameter 'id' cannot be null.");
            else if (id !== undefined)
                url_ += "id=" + encodeURIComponent("" + id) + "&";
            url_ = url_.replace(/[?&]$/, "");

            let options_: RequestInit = {
                method: "GET",
                headers: {
                    "Accept": "application/json"
                }
            };

            return this.http.fetch(url_, options_).then((_response: Response) => {
                return this.processGet(_response);
            });
        }

        protected processGet(response: Response): Promise<UsersDetailsModel> {
            const status = response.status;
            let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
            if (status === 200) {
                return response.text().then((_responseText) => {
                    let result200: any = null;
                    let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                    result200 = UsersDetailsModel.fromJS(resultData200);
                    return result200;
                });
            } else if (status !== 200 && status !== 204) {
                return response.text().then((_responseText) => {
                    return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                });
            }
            return Promise.resolve<UsersDetailsModel>(null as any);
        }

        getUserByEmail(userEmail: string | null | undefined): Promise<UsersDetailsModel> {
            let url_ = this.baseUrl + "/api/v1/User/GetUserByEmail?";
            if (userEmail !== undefined && userEmail !== null)
                url_ += "userEmail=" + encodeURIComponent("" + userEmail) + "&";
            url_ = url_.replace(/[?&]$/, "");

            let options_: RequestInit = {
                method: "GET",
                headers: {
                    "Accept": "application/json"
                }
            };

            return this.http.fetch(url_, options_).then((_response: Response) => {
                return this.processGetUserByEmail(_response);
            });
        }

        protected processGetUserByEmail(response: Response): Promise<UsersDetailsModel> {
            const status = response.status;
            let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
            if (status === 200) {
                return response.text().then((_responseText) => {
                    let result200: any = null;
                    let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                    result200 = UsersDetailsModel.fromJS(resultData200);
                    return result200;
                });
            } else if (status !== 200 && status !== 204) {
                return response.text().then((_responseText) => {
                    return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                });
            }
            return Promise.resolve<UsersDetailsModel>(null as any);
        }

        registerUser(user: UserCreateModel): Promise<number> {
            let url_ = this.baseUrl + "/api/v1/User/RegisterUserAsync";
            url_ = url_.replace(/[?&]$/, "");

            const content_ = JSON.stringify(user);

            let options_: RequestInit = {
                body: content_,
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            };

            return this.http.fetch(url_, options_).then((_response: Response) => {
                return this.processRegisterUser(_response);
            });
        }

        protected processRegisterUser(response: Response): Promise<number> {
            const status = response.status;
            let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
            if (status === 200) {
                return response.text().then((_responseText) => {
                    let result200: any = null;
                    let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                    result200 = resultData200 !== undefined ? resultData200 : <any>null;

                    return result200;
                });
            } else if (status !== 200 && status !== 204) {
                return response.text().then((_responseText) => {
                    return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                });
            }
            return Promise.resolve<number>(null as any);
        }

        update(id: number | undefined, roles: string | null | undefined): Promise<number> {
            let url_ = this.baseUrl + "/api/v1/User/UpdateAsync?";
            if (id === null)
                throw new Error("The parameter 'id' cannot be null.");
            else if (id !== undefined)
                url_ += "id=" + encodeURIComponent("" + id) + "&";
            if (roles !== undefined && roles !== null)
                url_ += "roles=" + encodeURIComponent("" + roles) + "&";
            url_ = url_.replace(/[?&]$/, "");

            let options_: RequestInit = {
                method: "PUT",
                headers: {
                    "Accept": "application/json"
                }
            };

            return this.http.fetch(url_, options_).then((_response: Response) => {
                return this.processUpdate(_response);
            });
        }

        protected processUpdate(response: Response): Promise<number> {
            const status = response.status;
            let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
            if (status === 200) {
                return response.text().then((_responseText) => {
                    let result200: any = null;
                    let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                    result200 = resultData200 !== undefined ? resultData200 : <any>null;

                    return result200;
                });
            } else if (status !== 200 && status !== 204) {
                return response.text().then((_responseText) => {
                    return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                });
            }
            return Promise.resolve<number>(null as any);
        }

        getHistoryTotalCount(id: number | undefined): Promise<number> {
            let url_ = this.baseUrl + "/api/v1/User/GetHistoryTotalCountAsync?";
            if (id === null)
                throw new Error("The parameter 'id' cannot be null.");
            else if (id !== undefined)
                url_ += "id=" + encodeURIComponent("" + id) + "&";
            url_ = url_.replace(/[?&]$/, "");

            let options_: RequestInit = {
                method: "GET",
                headers: {
                    "Accept": "application/json"
                }
            };

            return this.http.fetch(url_, options_).then((_response: Response) => {
                return this.processGetHistoryTotalCount(_response);
            });
        }

        protected processGetHistoryTotalCount(response: Response): Promise<number> {
            const status = response.status;
            let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
            if (status === 200) {
                return response.text().then((_responseText) => {
                    let result200: any = null;
                    let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                    result200 = resultData200 !== undefined ? resultData200 : <any>null;

                    return result200;
                });
            } else if (status !== 200 && status !== 204) {
                return response.text().then((_responseText) => {
                    return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                });
            }
            return Promise.resolve<number>(null as any);
        }

        getHistory(id: number | undefined, pageNumber: number | undefined, pageSize: number | undefined): Promise<UserHistoryModel[]> {
            let url_ = this.baseUrl + "/api/v1/User/GetHistoryAsync?";
            if (id === null)
                throw new Error("The parameter 'id' cannot be null.");
            else if (id !== undefined)
                url_ += "id=" + encodeURIComponent("" + id) + "&";
            if (pageNumber === null)
                throw new Error("The parameter 'pageNumber' cannot be null.");
            else if (pageNumber !== undefined)
                url_ += "pageNumber=" + encodeURIComponent("" + pageNumber) + "&";
            if (pageSize === null)
                throw new Error("The parameter 'pageSize' cannot be null.");
            else if (pageSize !== undefined)
                url_ += "pageSize=" + encodeURIComponent("" + pageSize) + "&";
            url_ = url_.replace(/[?&]$/, "");

            let options_: RequestInit = {
                method: "GET",
                headers: {
                    "Accept": "application/json"
                }
            };

            return this.http.fetch(url_, options_).then((_response: Response) => {
                return this.processGetHistory(_response);
            });
        }

        protected processGetHistory(response: Response): Promise<UserHistoryModel[]> {
            const status = response.status;
            let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
            if (status === 200) {
                return response.text().then((_responseText) => {
                    let result200: any = null;
                    let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                    if (Array.isArray(resultData200)) {
                        result200 = [] as any;
                        for (let item of resultData200)
                            result200!.push(UserHistoryModel.fromJS(item));
                    }
                    else {
                        result200 = <any>null;
                    }
                    return result200;
                });
            } else if (status !== 200 && status !== 204) {
                return response.text().then((_responseText) => {
                    return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                });
            }
            return Promise.resolve<UserHistoryModel[]>(null as any);
        }
    }

    export class UsersDetailsModel implements IUsersDetailsModel {
        id!: number;
        userName?: string | undefined;
        email?: string | undefined;
        firstName?: string | undefined;
        lastName?: string | undefined;
        roles?: string | undefined;
        lastLoginDateTime!: Date;

        constructor(data?: IUsersDetailsModel) {
            if (data) {
                for (var property in data) {
                    if (data.hasOwnProperty(property))
                        (<any>this)[property] = (<any>data)[property];
                }
            }
        }

        init(_data?: any) {
            if (_data) {
                this.id = _data["id"];
                this.userName = _data["userName"];
                this.email = _data["email"];
                this.firstName = _data["firstName"];
                this.lastName = _data["lastName"];
                this.roles = _data["roles"];
                this.lastLoginDateTime = _data["lastLoginDateTime"] ? new Date(_data["lastLoginDateTime"].toString()) : <any>undefined;
            }
        }

        static fromJS(data: any): UsersDetailsModel {
            data = typeof data === 'object' ? data : {};
            let result = new UsersDetailsModel();
            result.init(data);
            return result;
        }

        toJSON(data?: any) {
            data = typeof data === 'object' ? data : {};
            data["id"] = this.id;
            data["userName"] = this.userName;
            data["email"] = this.email;
            data["firstName"] = this.firstName;
            data["lastName"] = this.lastName;
            data["roles"] = this.roles;
            data["lastLoginDateTime"] = this.lastLoginDateTime ? this.lastLoginDateTime.toISOString() : <any>undefined;
            return data;
        }
    }

    export interface IUsersDetailsModel {
        id: number;
        userName?: string | undefined;
        email?: string | undefined;
        firstName?: string | undefined;
        lastName?: string | undefined;
        roles?: string | undefined;
        lastLoginDateTime: Date;
    }

    export class UserCreateModel implements IUserCreateModel {
        userName?: string | undefined;
        email?: string | undefined;
        firstName?: string | undefined;
        lastName?: string | undefined;
        roles?: string | undefined;

        constructor(data?: IUserCreateModel) {
            if (data) {
                for (var property in data) {
                    if (data.hasOwnProperty(property))
                        (<any>this)[property] = (<any>data)[property];
                }
            }
        }

        init(_data?: any) {
            if (_data) {
                this.userName = _data["userName"];
                this.email = _data["email"];
                this.firstName = _data["firstName"];
                this.lastName = _data["lastName"];
                this.roles = _data["roles"];
            }
        }

        static fromJS(data: any): UserCreateModel {
            data = typeof data === 'object' ? data : {};
            let result = new UserCreateModel();
            result.init(data);
            return result;
        }

        toJSON(data?: any) {
            data = typeof data === 'object' ? data : {};
            data["userName"] = this.userName;
            data["email"] = this.email;
            data["firstName"] = this.firstName;
            data["lastName"] = this.lastName;
            data["roles"] = this.roles;
            return data;
        }
    }

    export interface IUserCreateModel {
        userName?: string | undefined;
        email?: string | undefined;
        firstName?: string | undefined;
        lastName?: string | undefined;
        roles?: string | undefined;
    }

    export class HistoryModelBase implements IHistoryModelBase {
        sysStartTime!: Date;
        sysEndTime!: Date;

        constructor(data?: IHistoryModelBase) {
            if (data) {
                for (var property in data) {
                    if (data.hasOwnProperty(property))
                        (<any>this)[property] = (<any>data)[property];
                }
            }
        }

        init(_data?: any) {
            if (_data) {
                this.sysStartTime = _data["sysStartTime"] ? new Date(_data["sysStartTime"].toString()) : <any>undefined;
                this.sysEndTime = _data["sysEndTime"] ? new Date(_data["sysEndTime"].toString()) : <any>undefined;
            }
        }

        static fromJS(data: any): HistoryModelBase {
            data = typeof data === 'object' ? data : {};
            let result = new HistoryModelBase();
            result.init(data);
            return result;
        }

        toJSON(data?: any) {
            data = typeof data === 'object' ? data : {};
            data["sysStartTime"] = this.sysStartTime ? this.sysStartTime.toISOString() : <any>undefined;
            data["sysEndTime"] = this.sysEndTime ? this.sysEndTime.toISOString() : <any>undefined;
            return data;
        }
    }

    export interface IHistoryModelBase {
        sysStartTime: Date;
        sysEndTime: Date;
    }

    export class UserHistoryModel extends HistoryModelBase implements IUserHistoryModel {
        id!: number;
        userName?: string | undefined;
        email?: string | undefined;
        firstName?: string | undefined;
        lastName?: string | undefined;
        roles?: string | undefined;
        lastLoginDateTime!: Date;

        constructor(data?: IUserHistoryModel) {
            super(data);
        }

        init(_data?: any) {
            super.init(_data);
            if (_data) {
                this.id = _data["id"];
                this.userName = _data["userName"];
                this.email = _data["email"];
                this.firstName = _data["firstName"];
                this.lastName = _data["lastName"];
                this.roles = _data["roles"];
                this.lastLoginDateTime = _data["lastLoginDateTime"] ? new Date(_data["lastLoginDateTime"].toString()) : <any>undefined;
            }
        }

        static fromJS(data: any): UserHistoryModel {
            data = typeof data === 'object' ? data : {};
            let result = new UserHistoryModel();
            result.init(data);
            return result;
        }

        toJSON(data?: any) {
            data = typeof data === 'object' ? data : {};
            data["id"] = this.id;
            data["userName"] = this.userName;
            data["email"] = this.email;
            data["firstName"] = this.firstName;
            data["lastName"] = this.lastName;
            data["roles"] = this.roles;
            data["lastLoginDateTime"] = this.lastLoginDateTime ? this.lastLoginDateTime.toISOString() : <any>undefined;
            super.toJSON(data);
            return data;
        }
    }

    export interface IUserHistoryModel extends IHistoryModelBase {
        id: number;
        userName?: string | undefined;
        email?: string | undefined;
        firstName?: string | undefined;
        lastName?: string | undefined;
        roles?: string | undefined;
        lastLoginDateTime: Date;
    }

    export interface FileResponse {
        data: Blob;
        status: number;
        fileName?: string;
        headers?: { [name: string]: any };
    }

    export class ApiException extends Error {
        message: string;
        status: number;
        response: string;
        headers: { [key: string]: any; };
        result: any;

        constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
            super();

            this.message = message;
            this.status = status;
            this.response = response;
            this.headers = headers;
            this.result = result;
        }

        protected isApiException = true;

        static isApiException(obj: any): obj is ApiException {
            return obj.isApiException === true;
        }
    }

    function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
        if (result !== null && result !== undefined)
            throw result;
        else
            throw new ApiException(message, status, response, headers, null);
    }

}