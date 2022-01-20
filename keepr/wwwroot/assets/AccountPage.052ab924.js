import{_ as k,v as b,k as V,A as c,l as x,P as K}from"./index.071c3a57.js";import{C as M,c as n,a,o,b as r,d as e,t as d,j as _,F as i,l as m,e as u,D as p}from"./vendor.202f8bfe.js";const w={name:"Account",setup(){return M(async()=>{try{await b.getMyVaults(),await V.getByProfile(c.account.id),document.title="Keepr | Account"}catch(l){x.error(l),K.toast(l.message,"error")}}),{account:n(()=>c.account),vaults:n(()=>c.myVaults),keeps:n(()=>c.keeps)}}},C={class:"about p-0 container-fluid"},A={class:"row m-3"},B={class:"col d-flex"},P=["src"],S={class:"m-3"},j={class:"container-fluid"},N={class:"row"},D={class:"m-0 my-4 py-4 bg-primary"},F=e("i",{class:"mdi mdi-plus text-info selectable rounded","data-bs-toggle":"modal","data-bs-target":"#create-vault-modal",title:"create vault"},null,-1),E={class:"masonry-with-columns"},L={class:"container-fluid"},T={class:"row"},q={class:"m-0 my-4 py-4 bg-primary"},z=e("i",{class:"mdi mdi-plus text-info selectable rounded","data-bs-toggle":"modal","data-bs-target":"#create-keep-modal",title:"create keep"},null,-1),G={class:"masonry-with-columns"};function H(l,I,J,t,O,Q){const h=a("Vault"),v=a("Keep"),g=a("KeepModal"),f=a("CreateVaultModal"),y=a("CreateKeepModal");return o(),r(i,null,[e("div",C,[e("div",A,[e("div",B,[e("img",{class:"rounded-pill",src:t.account.picture,alt:""},null,8,P),e("h3",S,d(t.account.name),1)])])]),e("div",j,[e("div",N,[e("h1",D,[_(" Vaults: "+d(t.vaults.length)+" ",1),F]),e("div",E,[(o(!0),r(i,null,m(t.vaults,s=>(o(),p(h,{vault:s,key:s.id},null,8,["vault"]))),128))])])]),e("div",L,[e("div",T,[e("h1",q,[_(" Keeps: "+d(t.keeps.length)+" ",1),z]),e("div",G,[(o(!0),r(i,null,m(t.keeps,s=>(o(),p(v,{keep:s,key:s.id},null,8,["keep"]))),128))])])]),u(g),u(f),u(y)],64)}var W=k(w,[["render",H]]);export{W as default};
